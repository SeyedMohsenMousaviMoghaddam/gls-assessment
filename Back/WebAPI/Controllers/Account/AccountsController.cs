using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DAL;
using DAL.Infra;
using DAL.IService.Account;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Auth;

namespace WebAPI.Controllers.Account
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    
    public class AccountsController : Controller
    {
        private readonly IUserService _service;

        public AccountsController(IUserService service)
        {
            _service = service;            
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServerResult> ValidateToken([FromBody] string token)
        {
            return new ServerResult() { Success = await ValidateJwtToken(token) > 0 };
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {


            LoginValidator _validator = new LoginValidator();
            var validResult = _validator.Validate(model);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            var userResult = await _service.LoginUser(model.Username, model.Password);
            if (!userResult.Success)
            {
                validResult.Errors.Add(new FluentValidation.Results.ValidationFailure {ErrorMessage = userResult.Message });
                return BadRequest(validResult.Errors);
            }
            var token = GenerateToken(userResult.Data as User);
            return Ok(new ServerResult
            {
                Success = true,
                Data = token
            });
        }

        [HttpGet("{id}")]
        public async Task<ServerResult> GetRoleByUserId(int id)
        {
            return await _service.GetRoleByUserId(id);
        }


        [HttpPost("{id}")]
        public async Task<ServerResult> GetUserById(int id)
        {

            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshVM model)
        {
            try
            {
                var decoded = (new JwtSecurityTokenHandler()).ReadJwtToken(model.Token);
                var userResult = await _service.GetByUserName(decoded.Subject);
                if (!userResult.Success)
                    return new BadRequestResult();

                if (!(userResult.Data is User user))
                    return new BadRequestResult();

                var newToken = GenerateToken(user);
                return new OkObjectResult(newToken);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        private string GenerateToken(User user)
        {

            var token = new JwtTokenBuilder()
                                            .AddSecurityKey(ApplicationTokens.Tokens["AdminPanel"])
                                            .AddSubject(user.UserName)
                                            .AddIssuer("GLS")
                                            .AddAudience("AdminPanel")
                                            .AddClaim("username", user.UserName)
                                            .AddClaim("displayname", $"{user.Name}")
                                            .AddClaim("userId", user.Id.ToString())
                                            .AddClaim("roles", JsonConvert.SerializeObject(new string[] { user.Roles.Select(p=>p.Name).ToString() }))
                                            //.AddClaim("routes", JsonConvert.SerializeObject(roles?.Where(r => r.StateCode).Select(p => p.Path).Distinct().ToList()))
                                            .AddExpiry(1000)
                                            .Build();

            return token.Value;

        }

        private async Task<int> ValidateJwtToken(string token)
        {
            return await Task.Run(() =>
            {
                AuthenticationHeaderValue.TryParse(token,out var parsedValue);
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    tokenHandler.ValidateToken(parsedValue.Parameter, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKeys = ApplicationTokens.Tokens.Values,
                        ValidIssuer = "GLS",
                        ValidAudiences = ApplicationTokens.Tokens.Keys,
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var accountId = int.Parse(jwtToken.Claims.First(x => x.Type.ToLower() == "userId".ToLower()).Value);

                    return accountId;
                }
                catch (Exception e)
                {
                    return 0;
                }
            });
        }
    }
}