using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface ICommentService
    {
        void AddComment(int postId, int authorId, string content);

        void RemoveComments(Expression<Func<CommentEntity, bool>> predicate);

        List<CommentModel> GetComments(Expression<Func<CommentEntity, bool>> predicate);
    }
}
