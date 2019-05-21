using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book.Data.Entities;
using System;
using System.Collections.Generic;

namespace Book.Data.Entities
{
    public class BookEntity
    {
        /// <summary>
        /// 图书ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 售卖人ID。
        /// </summary>
        public int SellerId { get; set; }

        /// <summary>
        /// 图书名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 剩余数量。
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// 图书类型。
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 图片URL。
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 出版社。
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 出版时间。
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// 图书折旧程度。
        /// </summary>
        public int Depreciation { get; set; }

        /// <summary>
        /// 国际标准图书编号。
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 价格。
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 图书描述。
        /// </summary>
        public string Description { get; set; }

        public virtual UserEntity Seller { get; set; }

        /// <summary>
        /// 图书所在的订单。
        /// </summary>
        public virtual ICollection<OrderEntity> Orders { get; set; }

        public static void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).
                ValueGeneratedOnAdd();

            builder.HasOne(entity => entity.Seller).
                WithMany(entity => entity.Sells).
                HasForeignKey(entity => entity.SellerId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
