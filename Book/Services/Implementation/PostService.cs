using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Book.Data.Context;
using Book.Data.Entities;
using Book.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly MySqlContext _mySql;

        public PostService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public void AddPost(int authorId, string title, string content)
        {
            _mySql.Posts.AddAsync(new PostEntity
            {
                AuthorId = authorId,
                Title = title,
                Content = content,
            });
            _mySql.SaveChangesAsync();
        }

        public List<PostModel> GetPosts(Expression<Func<PostEntity, bool>> predicate)
        {
            var posts = _mySql.Posts.
                Where(predicate).
                ToArray();

            var res = new List<PostModel>();
            foreach (var post in posts)
            {
                var user = _mySql.Users.
                    FirstOrDefault(entity => entity.Id == post.AuthorId);
                res.Add(new PostModel(post)
                {
                    AuthorName = user.NickName,
                    ImageURL = user.ImageURL,
                    Level = user.ExpPoints
                });
            }
            return res;
        }

        public void RemovePosts(Expression<Func<PostEntity, bool>> predicate)
        {
            var posts = _mySql.Posts.
                Where(predicate).
                ToArray();
            _mySql.Posts.RemoveRange(posts);
            _mySql.SaveChangesAsync();
        }
    }

    public static class PostServiceExtensions
    {
        public static IServiceCollection AddPostService(this IServiceCollection services)
        {
            return services.AddScoped<IPostService, PostService>();
        }
    }
}
