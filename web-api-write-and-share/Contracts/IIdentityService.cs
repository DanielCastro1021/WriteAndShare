using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Entities;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Models;

namespace web_api_write_and_share.Contracts
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest registo);
        Task<AuthenticationResult> LoginAsync(UserLoginRequest userData);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetUsersAsync();
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> UpdateUserAsync(Guid user, UserUpdateRequest request);
        Task<bool> AddFriendAsync(Guid user, Guid friend);
        Task<bool> RemoveFriendAsync(Guid user, Guid friend);
        Task<List<User>> GetAllFriends(Guid user);
    }

}
