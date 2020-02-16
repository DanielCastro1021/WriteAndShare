using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Controllers.Response;
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

        public async Task<bool> AddPostAsync(Guid userId, NewPostRequest newpost)
        {
            byte[] uploadToBytes = null;

            if (newpost.Upload != null && newpost.Upload.Length > 0)
            {
                uploadToBytes = Convert.FromBase64String(newpost.Upload);
            } 

            Post post = new Post
            {
                Title = newpost.Title,
                Upload = uploadToBytes,
                Body = newpost.Body,
                Date = newpost.Date,
                TAGS = newpost.TAGS,
                Owner = userId,
                likes = 0
            };

            datacontext.Posts.Add(post);
            var added = await datacontext.SaveChangesAsync();

            return added > 0;
        }

        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Post> posts = await datacontext.Posts.AsNoTracking().ToListAsync();
            List<PostResponse> postResponses = new List<PostResponse>();

            for(int i=0; i<posts.Count; i++)
            {
                var temp = posts.ElementAt(i);
                var user = datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == temp.Owner);

                var post = new PostResponse
                {
                    Title = temp.Title,
                    Upload = Convert.ToBase64String(temp.Upload),
                    Body = temp.Body,
                    TAGS = temp.TAGS,
                    Owner = user.UserName,
                    Date = temp.Date,
                    likes = temp.likes
                };

                postResponses.Add(post);
            }

            return postResponses;
        }

        public async Task<List<PostResponse>> GetAllPostsByUserAsync(Guid userId)
        {
            List<Post> posts = await datacontext.Posts.AsNoTracking().Where(x => x.Owner == userId).ToListAsync();
            List<PostResponse> postResponses = new List<PostResponse>();
            var user = datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == userId);

            for (int i = 0; i < posts.Count; i++)
            {
                var temp = posts.ElementAt(i);

                var post = new PostResponse
                {
                    Title = temp.Title,
                    Upload = Convert.ToBase64String(temp.Upload),
                    Body = temp.Body,
                    TAGS = temp.TAGS,
                    Owner = user.UserName,
                    Date = temp.Date,
                    likes = temp.likes
                };

                postResponses.Add(post);
            }

            return postResponses;
        }

        public async Task<PostResponse> GetPostByIdAsync(Guid postId)
        {
            Post post = datacontext.Posts.AsNoTracking().SingleOrDefault(x => x.Id == postId);
            var user = datacontext.Users.AsNoTracking().SingleOrDefault(x => x.Id == post.Owner);

            var postResponse = new PostResponse
            {
                Title = post.Title,
                Upload = Convert.ToBase64String(post.Upload),
                Body = post.Body,
                TAGS = post.TAGS,
                Owner = user.UserName,
                Date = post.Date,
                likes = post.likes
            };

            return postResponse;
        }

        public async Task<bool> RemovePostAsync(Guid postId)
        {
            var post = datacontext.Posts.AsNoTracking().SingleOrDefault(x => x.Id == postId);

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
