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

        public async Task<bool> AddFriendAsync(Guid user, Guid friend)
        {

            if (datacontext.Friends.Any(x => x.Id == user && x.FriendOfUserId == friend))
            {
                return false;
            }

            Friends friendsConnection = new Friends
            {
                UserId = user,
                FriendOfUserId = friend
            };

            Friends otherConnection = new Friends
            {
                UserId = friend,
                FriendOfUserId = user
            };

            datacontext.Friends.Update(friendsConnection);
            datacontext.Friends.Update(otherConnection);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> RemoveFriendAsync(Guid user, Guid friend)
        {
            if (!datacontext.Friends.Any(x => x.UserId == user && x.FriendOfUserId == friend))
            {
                return false;
            }

            datacontext.Friends.Remove(await this.FindConnectionsId(user, friend));
            datacontext.Friends.Remove(await this.FindConnectionsId(friend, user));
            var deleted = await datacontext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<User>> GetAllFriends(Guid user)
        {
            Friends[] connections = await datacontext.Friends.AsNoTracking().Where(x => x.UserId == user).ToArrayAsync();
            List<User> friendsOfUser = new List<User>();

            for(int i=0; i< connections.Length; i++)
            {
                friendsOfUser.Add(await FindUser(connections[i].FriendOfUserId));
            }

            return friendsOfUser;
        }

        private async Task<Friends> FindConnectionsId(Guid user, Guid friend)
        {
            return datacontext.Friends.AsNoTracking().SingleOrDefault(x => x.UserId == user && x.FriendOfUserId == friend);
        }

        private async Task<User> FindUser(Guid user)
        {
            return datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == user);
        }
    }
}
