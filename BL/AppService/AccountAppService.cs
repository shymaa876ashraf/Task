using AutoMapper.Configuration;
using BL.Bases;
using BL.interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task.DAL.DbModels;
using Task.DTOs.DTOs;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace BL.AppService
{
    public class AccountAppService : BaseAppService
    {
        IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;
        public AccountAppService(
            IUnitOfWork theUnitOfWork,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration
           ) : base(theUnitOfWork)
        {
            this._configuration = configuration;
            this._roleManager = roleManager;
        }
        public List<UserDTO> GetAllAccounts()
        {
            return Mapper.Map<List<UserDTO>>(TheUnitOfWork.Account.GetAllAccounts());
        }
        public UserDTO GetAccountById(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
            return Mapper.Map<UserDTO>(TheUnitOfWork.Account.GetAccountById(id));
        }
        public bool DeleteAccount(string id)
        {
            if (id == null)
                throw new ArgumentNullException();
            bool result = false;
            User user = TheUnitOfWork.Account.GetAccountById(id);
            TheUnitOfWork.Account.Update(user);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public async Task<User> Find(string username, string password)
        {
            User user = await TheUnitOfWork.Account.Find(username, password);
            if (user != null)
                return user;
            return null;
        }
        public async Task<User> FindByName(string userName)
        {
            User user = await TheUnitOfWork.Account.FindByName(userName);
            if (user != null)
                return user;
            return null;
        }
        public async Task<bool> UpdatePassword(string userID, string newPassword)
        {
            User identityUser = await TheUnitOfWork.Account.FindById(userID);
            identityUser.PasswordHash = newPassword;
            return await TheUnitOfWork.Account.updatePassword(identityUser);
        }
        public async Task<bool> Update(UserDTO user)
        {

            User identityUser = await TheUnitOfWork.Account.FindByName(user.UserName);
            var oldPassword = identityUser.PasswordHash;
            Mapper.Map(user, identityUser);
            identityUser.PasswordHash = oldPassword;
            return await TheUnitOfWork.Account.UpdateAccount(identityUser);
        }
        public async Task<bool> checkUserNameExist(string userName)
        {
            var user = await TheUnitOfWork.Account.FindByName(userName);
            return user == null ? false : true;
        }
        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            return await TheUnitOfWork.Account.GetUserRoles(user);
        }
        public async Task<IdentityResult> Register(UserDTO user)
        {
            bool isExist = await checkUserNameExist(user.UserName);
            if (isExist)
                return IdentityResult.Failed(new IdentityError
                { Code = "error", Description = "user name already exist" });
            User identityUser = Mapper.Map<UserDTO, User>(user);
            var result = await TheUnitOfWork.Account.Register(identityUser);
            return result;
        }
        public int CountEntity()
        {
            return TheUnitOfWork.Account.CountEntity();
        }
        public IEnumerable<UserDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<UserDTO>>(TheUnitOfWork.Account.GetPageRecords(pageSize, pageNumber));
        }
    }
}
