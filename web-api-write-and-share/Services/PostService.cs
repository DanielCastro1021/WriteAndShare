using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Services
{
    public class PostService : IPostService
    {
        public Task<bool> AddPostAsync(NewPostRequest newpost)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
