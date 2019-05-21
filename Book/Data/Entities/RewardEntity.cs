using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data.Entities
{
    public class RewardEntity
    {
        /// <summary>
        /// 悬赏ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发布人ID。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 悬赏图书名称。
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 出版社。
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 图书分类。
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 国际标准图书编号。
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 悬赏价格。
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 图片URL。
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 出版时间。
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// 折旧程度。
        /// </summary>
        public int Depreciation { get; set; }

        /// <summary>
        /// 图书描述。
        /// </summary>
        public string Description { get; set; }

        public virtual UserEntity User { get; set; }

        public static void Configure(EntityTypeBuilder<RewardEntity> builder)
        {
            builder.ToTable("Reward");

            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).
                ValueGeneratedOnAdd();

            builder.HasOne(entity => entity.User).
                WithMany(entity => entity.Rewards).
                HasForeignKey(entity => entity.UserId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
