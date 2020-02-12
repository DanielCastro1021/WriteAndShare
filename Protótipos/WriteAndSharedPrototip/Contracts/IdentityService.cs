using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndSharedPrototip.Controllers.Request;
using WriteAndSharedPrototip.Models;

namespace WriteAndSharedPrototip.Contracts
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest registo);
        Task<AuthenticationResult> LoginAsync(UserLoginRequest userData);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetUsersAsync();
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> UpdateUserAsync(Guid user, UpdateUserRequest request);


    }
}
