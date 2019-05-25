using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class CommentModel
    {
        /// <summary>
        /// 发布评论的用户昵称。
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 评论内容。
        /// </summary>
        public string Content { get; set; }

        public string ImageURL { get; set; }

        public int Level { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime TimeStamp { get; set; }

        public CommentModel(CommentEntity comment)
        {
            Content = comment.Content;
            TimeStamp = comment.TimeStamp;
        }
    }
}
