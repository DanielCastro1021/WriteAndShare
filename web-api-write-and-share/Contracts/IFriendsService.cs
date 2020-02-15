using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Contracts
{
    public interface IFriendsService
    {
        Task<bool> AddFriendAsync(Guid user, Guid friend);
        Task<bool> RemoveFriendAsync(Guid user, Guid friend);
        Task<List<User>> GetAllFriends(Guid user);
    }
}
