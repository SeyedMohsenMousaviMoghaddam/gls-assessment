using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using DAL.Infra;
using DAL.Interfaces;
using DAL.IService.Account;
using DAL.Models;
using DAL.ViewModels;
using Serilog;

namespace DAL.Service.Account
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IUserLoginLogRepository _userLoginLogRepository;
        protected readonly IRoleRepository _roleRepository;

        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository,IUserLoginLogRepository userLoginLogRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userLoginLogRepository = userLoginLogRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ServerResult> GetByUserName(string username)
        {
            return new ServerResult { Success = true, Data = await _userRepository.GetByUserName(username) };

        }
        public async Task<ServerResult> LoginUser(string userName, string password, bool customerAgentLogin = true)
        {
            try
            {
                var userResult = await GetByUserName(userName).ConfigureAwait(false);


                if (userResult.Success == false)
                    return new ServerResult { Success = false, Message = "The username or password is wrong!" };

                var user = userResult.Data as User;

                var hashedPassword = Security.HashSHA1(password + user.UserName);

                if (hashedPassword == user.Password)
                {
                    await _userLoginLogRepository.Add(new Models.UserLoginLog { UserId = user.Id });
                    return new ServerResult { Success = true, Message = "Login successfully.", Data = user };
                }
                return new ServerResult { Success = false, Message = "The username or password is wrong!" };
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<ServerResult> Get(DatatableRequestVM model)
        {
            try
            {
                return new ServerResult { Success = true, Data = await _userRepository.GetAll() };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ServerResult> GetLog(int userId)
        {
            try
            {
                return new ServerResult { Success = true, Data = await _userLoginLogRepository.GetLog(userId) };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ServerResult> GetById(int id)
        {
            return new ServerResult { Success = true, Data = await _userRepository.GetById(id) };

        }

        public async Task<ServerResult> ChangePassword(ChangePasswordVM item, int userId)
        {
            var userResult = await GetById(item.UserId);
            if (userResult.Success == false)
                return new ServerResult { Success = false, Message = "User not found!" };
            var user = userResult.Data as User;
            var oldpasshashed = Security.HashSHA1(item.OldPassword + user.UserName);
            if (oldpasshashed != user.Password)
                return new ServerResult { Success = false, Message = "The old password is wrong!" };
            var newpassHashed = Security.HashSHA1(item.NewPassword + user.UserName);
            try
            {
                user.Password = newpassHashed;
                await _userRepository.Update(user);
                return new ServerResult { Success = true, Message = "successfully.", Data = user };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServerResult> Save(UserVM item, int userId)
        {
            try
            {
                var user = _mapper.Map<User>(item);

                user.Password = Security.HashSHA1(user.Password + user.UserName);
                return new ServerResult { Success = true, Data = await _userRepository.Add(user) };

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServerResult> Delete(int id)
        {
            try
            {
                await _userRepository.Remove(await _userRepository.GetById(id));
                return new ServerResult { Success = true };
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<ServerResult> GetRoleByUserId(int id)
        {
            var result = new ServerResult();
            result.Data = (await _userRepository.GetById(id)).Roles;
            return result;
        }

        public async Task<ServerResult> AddRoleForUser(int roleId, int userId)
        {
            try
            {
                var roleItem = await _roleRepository.GetById(roleId);
                var userItem = await _userRepository.GetById(userId);
                userItem.Roles.Add(roleItem);
                return new ServerResult { Success = true};

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServerResult> RemoveRoleFromUser(int roleId, int userId)
        {
            try
            {
                var roleItem = await _roleRepository.GetById(roleId);
                var userItem = await _userRepository.GetById(userId);
                //userItem.RoleItems.(roleItem);
                return new ServerResult { Success = true };

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
