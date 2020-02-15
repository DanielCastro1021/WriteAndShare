using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        [HttpPost(ApiRoutes.Identity.AddPost)]
        public async Task<IActionResult> AddPost(Guid userId, [FromBody] NewPostRequest newpost)
        {
           
            var added = await postService.AddPostAsync(userId, newpost);

            if (added)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete(ApiRoutes.Identity.RemovePost)]
        public async Task<IActionResult> RemovePost(Guid postId)
        {
            var removed = await postService.RemovePostAsync(postId);

            if (removed)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.Identity.GetPostById)]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var post = await postService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }


            return Ok(post);
        }

        [HttpGet(ApiRoutes.Identity.GetPostsByUser)]
        public async Task<IActionResult> GetAllPostsByUser(Guid userId)
        {
            List<Post> postsList = await postService.GetAllPostsByUserAsync(userId);

            if (postsList.Count == 0)
            {
                return NoContent();
            }

            return Ok(postsList);
        }

        [HttpGet(ApiRoutes.Identity.GetAllPosts)]
        public async Task<IActionResult> GetAllPost()
        {
            List<Post> postsList = await postService.GetAllPostsAsync();

            if (postsList.Count == 0)
            {
                return NoContent();
            }

            return Ok(postsList);
        }

        [HttpPut(ApiRoutes.Identity.AddLikeToPostById)]
        public async Task<IActionResult> AddLikeToPost(Guid postId)
        {
            var liked = await postService.AddLikeToPostAsync(postId);

            if (liked)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
