using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsService friendsService;

        public FriendsController(IFriendsService _friendsService)
        {
            friendsService = _friendsService;
        }

        [HttpPut(ApiRoutes.Identity.AddFriend)]
        public async Task<IActionResult> AddFriend(Guid userid, Guid userToAdd)
        {
            var added = await friendsService.AddFriendAsync(userid, userToAdd);

            if (added)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete(ApiRoutes.Identity.RemoveFriend)]
        public async Task<IActionResult> RemoveFriend(Guid userid, Guid userToRemove)
        {
            var removed = await friendsService.RemoveFriendAsync(userid, userToRemove);

            if (removed)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.Identity.GetAllFriends)]
        public async Task<IActionResult> GetAllFriends(Guid userid)
        {
            List<User> friendsList = await friendsService.GetAllFriends(userid);

            if(friendsList.Count == 0)
            {
                return NoContent();
            }

            return Ok(friendsList);
        }
    }
}
