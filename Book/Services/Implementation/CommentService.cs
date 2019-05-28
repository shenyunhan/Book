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
    public class CommentService : ICommentService
    {
        private readonly MySqlContext _mySql;

        public CommentService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public void AddComment(int postId, int authorId, string content)
        {
            _mySql.Comments.AddAsync(new CommentEntity
            {
                PostId = postId,
                AuthorId = authorId,
                Content = content
            });

            var user = _mySql.Users.FirstOrDefault(entity => entity.Id == authorId);
            user.ExpPoints += 1;
            _mySql.Users.Update(user);

            _mySql.SaveChangesAsync();
        }

        public List<CommentModel> GetComments(Expression<Func<CommentEntity, bool>> predicate)
        {
            var comments = _mySql.Comments.
                Where(predicate).
                ToArray();

            var res = new List<CommentModel>();
            foreach (var comment in comments)
            {
                var user = _mySql.Users.
                    FirstOrDefault(entity => entity.Id == comment.AuthorId);
                res.Add(new CommentModel(comment)
                {
                    AuthorName = user.NickName,
                    ImageURL = user.ImageURL,
                    Level = user.ExpPoints
                });
            }
            return res;
        }

        public void RemoveComments(Expression<Func<CommentEntity, bool>> predicate)
        {
            var comments = _mySql.Comments.
                Where(predicate).
                ToArray();
            _mySql.Comments.RemoveRange(comments);
            _mySql.SaveChangesAsync();
        }
    }

    public static class CommentServiceExtensions
    {
        public static IServiceCollection AddCommentService(this IServiceCollection service)
        {
            return service.AddScoped<ICommentService, CommentService>();
        }
    }
}
