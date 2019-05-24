using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IPostService
    {
        void AddPost(int authorId, string title, string content);

        void RemovePosts(Expression<Func<PostEntity, bool>> predicate);

        List<PostModel> GetPosts(Expression<Func<PostEntity, bool>> predicate);
    }
}
