using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Data;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly DataContext datacontext;

        public FriendsService(DataContext _datacontext)
        {
            datacontext = _datacontext;
        }

        public async Task<bool> AddFriendAsync(Guid userId, Guid friendId)
        {

            if (datacontext.Friends.Any(x => x.Id == userId && x.FriendOfUserId == friendId))
            {
                return false;
            }

            Friends friendsConnection = new Friends
            {
                UserId = userId,
                FriendOfUserId = friendId
            };

            Friends otherConnection = new Friends
            {
                UserId = friendId,
                FriendOfUserId = userId
            };

            datacontext.Friends.Add(friendsConnection);
            datacontext.Friends.Add(otherConnection);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> RemoveFriendAsync(Guid userId, Guid friendId)
        {
            if (!datacontext.Friends.Any(x => x.UserId == userId && x.FriendOfUserId == friendId))
            {
                return false;
            }

            datacontext.Friends.Remove(await this.FindConnectionsId(userId, friendId));
            datacontext.Friends.Remove(await this.FindConnectionsId(friendId, userId));
            var deleted = await datacontext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<User>> GetAllFriends(Guid user)
        {
            List<Friends> connections = await datacontext.Friends.AsNoTracking().Where(x => x.UserId == user).ToListAsync();
            List<User> friendsOfUser = new List<User>();

            int i = 0;
            while(i < connections.Count)
            {
                friendsOfUser.Add(await this.FindUser(connections[i].FriendOfUserId));
                i++;
            }

            return friendsOfUser;
        }

        private async Task<Friends> FindConnectionsId(Guid userId, Guid friendId)
        {
            return datacontext.Friends.AsNoTracking().SingleOrDefault(x => x.UserId == userId && x.FriendOfUserId == friendId);
        }

        private async Task<User> FindUser(Guid userId)
        {
            return datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == userId);
        }
    }
}
