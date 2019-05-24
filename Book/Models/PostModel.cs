using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class PostModel
    {
        /// <summary>
        /// 讨论帖ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发布者昵称。
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 讨论帖标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 讨论帖内容。
        /// </summary>
        public string Content { get; set; }

        public string ImageURL { get; set; }

        /// <summary>
        /// 发帖时间。
        /// </summary>
        public DateTime TimeStamp { get; set; }

        public PostModel(PostEntity post)
        {
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            TimeStamp = post.TimeStamp;
        }
    }
}
