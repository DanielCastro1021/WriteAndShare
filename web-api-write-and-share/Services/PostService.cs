using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Data;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext datacontext;

        public PostService(DataContext _datacontext)
        {
            datacontext = _datacontext;
        }

        public async Task<bool> AddPostAsync(NewPostRequest newpost)
        {
            Post post = new Post
            {
                Title = newpost.Title,
                Upload = newpost.Upload,
                Body = newpost.Body,
                Date = newpost.Date,
                TAGS = newpost.TAGS,
                userId = newpost.Owner,
                likes = 0
            };

            datacontext.Posts.Add(post);
            var added = await datacontext.SaveChangesAsync();

            return added > 0;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await datacontext.Posts.AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return datacontext.Posts.AsNoTracking().SingleOrDefault(x => x.Id == postId);
        }

        public async Task<bool> RemovePostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);

            datacontext.Posts.Remove(post);
            var deleted = await datacontext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> AddLikeToPostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);
            long likes = post.likes;

            post.likes = likes + 1;
            datacontext.Update(post);
            var updated = await datacontext.SaveChangesAsync();

            return updated > 0;

        }
    }
}
