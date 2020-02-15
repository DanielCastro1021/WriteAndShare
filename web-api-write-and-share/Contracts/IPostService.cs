using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Contracts
{
    public interface IPostService
    {
        Task<bool> AddPostAsync(Guid userId, NewPostRequest newpost);
        Task<bool> RemovePostAsync(Guid postId);
        Task<Post> GetPostByIdAsync(Guid postId);
        Task<List<Post>> GetAllPostsAsync();
        Task<bool> AddLikeToPostAsync(Guid postId);
    }
}
