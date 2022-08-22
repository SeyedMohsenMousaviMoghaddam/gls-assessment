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
            var result = new ServerResult();
            result.Data = await _userRepository.GetByUserName(username);
            return result;
        }
        public async Task<ServerResult> LoginUser(string userName, string password, bool customerAgentLogin = true)
        {
            var userResult = await GetByUserName(userName).ConfigureAwait(false);


            if (userResult.Success == false)
                return new ServerResult { Success = false, Message = "نام کاربری یا رمزعبور اشتباه است." };

            var user = userResult.Data as UserVM;

            var hashedPassword = Security.HashSHA1(password + user.UserName);

            if (hashedPassword == user.Password)
            {
                await _userLoginLogRepository.Add(new Models.UserLoginLog { UserName=userName });
                return new ServerResult { Success = true, Message = "ورود با موفقیت.", Data = user };
            }
            return new ServerResult { Success = false, Message = "نام کاربری یا رمزعبور اشتباه است." };
        }

        public async Task<ServerResult> Get(DatatableRequestVM model)
        {
            try
            {
                //        using (var con = await Connection.GetOpenConnection(true))
                //        {
                //            var count = 0;
                //            var parameters = new DynamicParameters();
                //            parameters.Add("@PageNumber", model.PageNumber, DbType.Int32);
                //            parameters.Add("@RowNumber", model.PageSize, DbType.Int32);
                //            parameters.Add("@Filter", model.GetWhereClause<UserVM>(), DbType.String);
                //            parameters.Add("@TotalRecord", count, DbType.Int32, ParameterDirection.InputOutput);
                //            if (!string.IsNullOrEmpty(model.SortField))
                //                parameters.Add("@OrderBy", model.SortField, DbType.String);

                //            var output = (await con.QueryAsync<UserVM>("Get_User", parameters, commandType: CommandType.StoredProcedure)).ToList();
                //            count = parameters.Get<int>("@TotalRecord");

                //            var dt = new DataTableResult
                //            {
                //                CurrentPageNumber = model.PageNumber,
                //                Items = output,
                //                PageSize = model.PageSize,
                //                TotalRecords = count
                //            };

                //            return new ServerResult { Success = true, Data = dt };
                //        }
                return new ServerResult { Success = true, Data = await _userRepository.GetAll() };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServerResult> GetById(int id)
        {
            var result = new ServerResult();
            result.Data = await _userRepository.GetById(id);
            return result;
        }

        //public async Task<ServerResult> GetLookup(LookupDTO item)
        //{
        //    using (var con = await Connection.GetOpenConnection(true))
        //    {
        //        try
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@Filter", item.Filter, DbType.String);

        //            if (!string.IsNullOrEmpty(item.OrderBy))
        //                parameters.Add("@OrderBy", item.OrderBy, DbType.String);

        //            var output = (await con.QueryAsync<LookupItem>("Lookup_User", parameters, commandType: CommandType.StoredProcedure))
        //                .ToList();

        //            return new ServerResult { Success = true, Data = output };
        //        }
        //        catch (Exception e)
        //        {
        //            return e.RaiseError();
        //        }
        //    }
        //}


        //public async Task<ServerResult> ChangePassword(ChangePasswordCA item, int userId)
        //{
        //    var userResult = await GetById(userId);
        //    if (userResult.Success == false)
        //        return new ServerResult { Success = false, Message = "کاربر یافت نشد" };
        //    var user = userResult.Data as UserVM;
        //    var oldpasshashed = Security.HashSHA1(item.OldPassword + user.UserGuid);
        //    if (oldpasshashed != user.Password)
        //        return new ServerResult { Success = false, Message = "پسورد قدیم اشتباه است." };
        //    var newpassHashed = Security.HashSHA1(item.NewPassword + user.UserGuid);
        //    try
        //    {
        //        using (var con = await Connection.GetOpenConnection())
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@Id", user.Id, DbType.Int32);
        //            parameters.Add("@Password", newpassHashed, DbType.String);
        //            parameters.Add("@ModifiedBy", user.Id, DbType.Int32);
        //            //todo @ehsan c2e
        //            await con.ExecuteAsync("Save_UserChangePassword", parameters, commandType: CommandType.StoredProcedure);
        //            return new ServerResult { Success = true };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.RaiseError();
        //    }
        //}

        //public async Task<ServerResult> ChangePassword(ChangePasswordCA item)
        //{
        //    var userResult = await GetByUserName(item.UserName);
        //    if (userResult.Success == false)
        //        return new ServerResult { Success = false, Message = "کاربر یافت نشد" };
        //    var user = userResult.Data as UserVM;
        //    //var oldpasshashed = Security.HashSHA1(item.OldPassword + user.UserGuid);
        //    //if (oldpasshashed != user.Password)
        //    //    return new ServerResult { Success = false, Message = "پسورد قدیم اشتباه است." };
        //    var newpassHashed = Security.HashSHA1(item.NewPassword + user.UserGuid);
        //    try
        //    {
        //        using (var con = await Connection.GetOpenConnection())
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@Id", user.Id, DbType.Int32);
        //            parameters.Add("@Password", newpassHashed, DbType.String);
        //            parameters.Add("@ModifiedBy", user.Id, DbType.Int32);
        //            //todo @ehsan c2e
        //            await con.ExecuteAsync("Save_UserChangePassword", parameters, commandType: CommandType.StoredProcedure);
        //            return new ServerResult { Success = true };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.RaiseError();
        //    }
        //}

        //public async Task<ServerResult> ResetPassword(string userName)
        //{
        //    if (userName == null)
        //        return new ServerResult { Success = false, Message = "نام کاربری تهی است." };
        //    var ca = await GetByUserName(userName);
        //    if (ca == null)
        //        return new ServerResult { Success = false, Message = "سفیری با این نام کاربری یافت نشد." };
        //    var user = ca.Data as UserVM;

        //    _validationCodeService = new ValidationCodeService();
        //    return await _validationCodeService.Save(new ValidationCodeRequestVm
        //    {
        //        CreateReason = ValidationCodeCreateReason.ForgetPassword,
        //        IsUser = true,
        //        RelatedId = user.Id,
        //        SendType = ValidationCodeSendType.SMS
        //    });
        //}

        //public async Task<ServerResult> ConfirmResetPassword(string userName, string code, string newpassword)
        //{
        //    if (userName == null)
        //        return new ServerResult { Success = false, Message = "نام کاربری تهی است." };
        //    if (string.IsNullOrEmpty(newpassword))
        //        return new ServerResult { Success = false, Message = "رمزعبور جدید را وارد نمایید." };
        //    var ca = await GetByUserName(userName);
        //    if (ca == null)
        //        return new ServerResult { Success = false, Message = "سفیری با این نام کاربری یافت نشد." };
        //    var user = ca.Data as UserVM;

        //    _validationCodeService = new ValidationCodeService();
        //    var result = await _validationCodeService.GetLastCode(user.Id, true, ValidationCodeSendType.SMS);
        //    if (result.Success == false)
        //        return new ServerResult { Success = false, Message = "کد فعال سازی یافت نشد." };
        //    var lastCode = result.Data as ValidationCodeVm;
        //    if (lastCode.Code == code && DateTime.Now <= lastCode.ExpiryTime && !lastCode.IsUsed)
        //        try
        //        {
        //            var newpassHashed = Security.HashSHA1(newpassword + user.UserGuid);
        //            using (var con = await Connection.GetOpenConnection())
        //            {
        //                var parameters = new DynamicParameters();
        //                parameters.Add("@Id", user.Id, DbType.Int32);
        //                parameters.Add("@Password", newpassHashed, DbType.String);
        //                parameters.Add("@ModifiedBy", user.Id, DbType.Int32);
        //                //todo @ehsan c2e
        //                await con.ExecuteAsync("Save_UserChangePassword", parameters, commandType: CommandType.StoredProcedure);

        //                parameters = new DynamicParameters();
        //                parameters.Add("@Id", user.Id, DbType.Int32);
        //                parameters.Add("@IsUser", 1, DbType.Boolean);
        //                parameters.Add("@Code", code, DbType.String);
        //                //todo @ehsan c2e
        //                await con.ExecuteAsync("SP_SetValidationCodeIsUsed", parameters, commandType: CommandType.StoredProcedure);
        //                return new ServerResult { Success = true };
        //            }


        //        }
        //        catch (Exception e)
        //        {
        //            return e.RaiseError();
        //        }
        //    return new ServerResult { Success = false, Message = "عدم تطابق کد" };
        //}

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
