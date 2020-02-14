using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Data;
using web_api_write_and_share.Entities;
using web_api_write_and_share.Helpers;
using web_api_write_and_share.Models;

namespace web_api_write_and_share.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings jwt;
        private readonly DataContext datacontext;

        public IdentityService(JwtSettings _jwt, DataContext _datacontext)
        {
            jwt = _jwt;
            datacontext = _datacontext;
        }

        public async Task<AuthenticationResult> LoginAsync(UserLoginRequest request)
        {
            var user = datacontext.Users.SingleOrDefault(x => x.UserName == request.UserName);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))

                return new AuthenticationResult
                {
                    Errors = new[] { "User/ password combination is wrong" }
                };

            return GenerateAuthenticationResult(user);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return datacontext.Users.SingleOrDefault(x => x.Id == userId);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await datacontext.Users.ToListAsync();
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest request)
        {

            if (datacontext.Users.Any(x => x.UserName == request.UserName))
                return new AuthenticationResult
                {
                    Errors = new[] { "O username que inseriu já se encontra em Uso" }
                };

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);


            var newUser = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                FriendsList = "",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            datacontext.Users.Add(newUser);
            datacontext.SaveChanges();

            return GenerateAuthenticationResult(newUser);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);

            datacontext.Users.Remove(user);
            var deleted = await datacontext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateUserAsync(Guid user, UserUpdateRequest dataToUpdate)
        {

            var userToUpdate = await GetUserByIdAsync(user);

            var userToUpdateActualName = userToUpdate.UserName;

            if (!(userToUpdateActualName == dataToUpdate.UserName))
            {
                if (datacontext.Users.Any(x => x.UserName == dataToUpdate.UserName))
                {
                    return false;
                }
            }

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(dataToUpdate.Password, out passwordHash, out passwordSalt);


            userToUpdate = new User
            {
                UserName = dataToUpdate.UserName,
                Id = userToUpdate.Id,
                Email = dataToUpdate.Email,
                FriendsList = userToUpdate.FriendsList,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Roles = null
            };

            datacontext.Users.Update(userToUpdate);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;
        }


        private AuthenticationResult GenerateAuthenticationResult(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwt.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
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

            friendsIds = friendsIds.Replace(friend.ToString(), "");

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

            for(int i=0; i<friendsIds.Length; i++)
            {
                friends.Add(await this.GetUserByIdAsync(Guid.Parse(friendsIds[i])));
            }

            return friends;
        }
    }

}
