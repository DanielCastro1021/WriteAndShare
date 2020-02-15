using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Data;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Services
{
    public class FriendsService
    {
        private readonly DataContext datacontext;
        private IdentityService identityService;

        public FriendsService(DataContext _datacontext)
        {
            datacontext = _datacontext;
        }

        public async Task<bool> AddFriendAsync(Guid user, Guid friend)
        {
            User requester = await this.GetUserByIdAsync(user);
            string friendsIds = requester.FriendsList;


            if (friendsIds.Contains(friend.ToString()))
            {
                return false;
            }

            friendsIds = friendsIds + friend.ToString() + ";";

            User userToUpdate = new User
            {
                UserName = requester.UserName,
                Id = requester.Id,
                Email = requester.Email,
                FriendsList = friendsIds,
                PasswordHash = requester.PasswordHash,
                PasswordSalt = requester.PasswordSalt,
                Roles = null
            };

            datacontext.Users.Update(userToUpdate);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> RemoveFriendAsync(Guid user, Guid friend)
        {
            User requester = await this.GetUserByIdAsync(user);
            string friendsIds = requester.FriendsList;

            if (!friendsIds.Contains(friend.ToString()))
            {
                return false;
            }

            friendsIds = friendsIds.Replace(friend.ToString() + ";", "");

            User userToUpdate = new User
            {
                UserName = requester.UserName,
                Id = requester.Id,
                Email = requester.Email,
                FriendsList = friendsIds,
                PasswordHash = requester.PasswordHash,
                PasswordSalt = requester.PasswordSalt,
                Roles = null
            };

            datacontext.Users.Update(userToUpdate);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<List<User>> GetAllFriends(Guid user)
        {
            List<User> friends = new List<User>();
            User requester = await this.GetUserByIdAsync(user);

            string[] friendsIds = requester.FriendsList.Split(";");

            for (int i = 0; i < friendsIds.Length; i++)
            {
                friends.Add(datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == new Guid(friendsIds[i])));
            }

            return friends;
        }
    }
}
