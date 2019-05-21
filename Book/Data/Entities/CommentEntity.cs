using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Book.Data.Entities
{
    public class CommentEntity
    {
        /// <summary>
        /// 评论ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 被评论讨论帖ID。
        /// </summary>
        public int PostId { get; set; }
        
        /// <summary>
        /// 发布评论的用户ID。
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 评论内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        [Timestamp]
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// 被评论的讨论帖。
        /// </summary>
        public virtual PostEntity Post { get; set; }

        /// <summary>
        /// 评论用户。
        /// </summary>
        public virtual UserEntity Author { get; set; }

        public static void Congifure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).
                ValueGeneratedOnAdd();

            builder.HasOne(entity => entity.Author).
                WithMany(entity => entity.Comments).
                HasForeignKey(entity => entity.AuthorId).
                OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entity => entity.Post).
                WithMany(entity => entity.Comments).
                HasForeignKey(entity => entity.PostId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
