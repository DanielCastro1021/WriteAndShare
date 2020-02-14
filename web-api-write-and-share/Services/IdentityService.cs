using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Entities;
using web_api_write_and_share.Models;

namespace web_api_write_and_share.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> LoginAsync(UserLoginRequest userData)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest registo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(Guid user, UserUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
