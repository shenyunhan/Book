using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book.Data.Entities
{
    public class PostEntity
    {
        /// <summary>
        /// 讨论帖ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发布者ID。
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 讨论帖标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 讨论帖内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发帖时间。
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 发布者。
        /// </summary>
        public virtual UserEntity Author { get; set; }

        /// <summary>
        /// 所有评论。
        /// </summary>
        public virtual ICollection<CommentEntity> Comments { get; set; }

        public static void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).
                ValueGeneratedOnAdd();

            builder.Property(entity => entity.TimeStamp)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<DateTimeGenerator>();

            builder.HasOne(entity => entity.Author).
                WithMany(entity => entity.Posts).
                HasForeignKey(entity => entity.AuthorId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
