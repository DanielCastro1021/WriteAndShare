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
            var user = datacontext.Users.AsNoTracking().SingleOrDefault(x => x.UserName == request.UserName);

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
            return datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == userId);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await datacontext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest request)
        {

            if (datacontext.Users.AsNoTracking().Any(x => x.UserName == request.UserName))
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
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
            };

            datacontext.Users.Add(newUser);
            datacontext.SaveChanges();

            return GenerateAuthenticationResult(newUser);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);

            await this.DeleteConnectionsOfUserAsync(userId);
            datacontext.Users.Remove(user);
            var deleted = await datacontext.SaveChangesAsync();
            return deleted > 0;
        }

        private async Task<bool> DeleteConnectionsOfUserAsync(Guid id)
        {
            List<Friends> connections = await datacontext.Friends.AsNoTracking().Where(x => x.UserId == id || x.FriendOfUserId == id).ToListAsync();

            int i = 0;
            while (i < connections.Count)
            {
                datacontext.Remove(connections[i]);
                i++;
            }

            var deleted = await datacontext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateUserAsync(Guid user, UserUpdateRequest dataToUpdate)
        {

            var userToUpdate = await GetUserByIdAsync(user);

            var userToUpdateActualName = userToUpdate.UserName;

            if (!(userToUpdateActualName == dataToUpdate.UserName))
            {
                if (datacontext.Users.AsNoTracking().Any(x => x.UserName == dataToUpdate.UserName))
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
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = userToUpdate.Role
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
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.Name, user.Id.ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.Role)
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

    }

}
