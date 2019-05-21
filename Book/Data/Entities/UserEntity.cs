using Book.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data.Entities
{
    public class UserEntity
    {
        /// <summary>
        /// 用户ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户的微信openID。
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户昵称。
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户论坛经验值。
        /// </summary>
        public int ExpPoints { get; set; }

        /// <summary>
        /// 用户头像URL。
        /// </summary>
        public string ImageURL { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }

        public virtual ICollection<PostEntity> Posts { get; set; }

        public virtual ICollection<CommentEntity> Comments { get; set; }

        public virtual ICollection<BookEntity> Sells { get; set; }

        public virtual ICollection<RewardEntity> Rewards { get; set; }

        public static void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(entity => entity.Id);

            builder.HasIndex(entity => entity.OpenId).
                IsUnique();
        }
    }
}
