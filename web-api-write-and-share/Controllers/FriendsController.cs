using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : Controller
    {
        private readonly IFriendsService friendsService;

        public FriendsController(IFriendsService _friendsService)
        {
            friendsService = _friendsService;
        }


        [HttpPut(ApiRoutes.Identity.AddFriend)]
        public async Task<IActionResult> AddFriend(Guid userId, Guid friendId)
        {
            var added = await friendsService.AddFriendAsync(userId, friendId);

            if (added)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete(ApiRoutes.Identity.RemoveFriend)]
        public async Task<IActionResult> RemoveFriend(Guid userId, Guid friendId)
        {
            var removed = await friendsService.RemoveFriendAsync(userId, friendId);

            if (removed)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.Identity.GetAllFriends)]
        public async Task<IActionResult> GetAllFriends(Guid userId)
        {
            List<User> friendsList = await friendsService.GetAllFriends(userId);

            if(friendsList.Count == 0)
            {
                return NoContent();
            }

            return Ok(friendsList);
        }
    }
}
