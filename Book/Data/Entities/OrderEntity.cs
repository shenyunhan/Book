using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book.Data.Entities
{
    public class OrderEntity
    {
        /// <summary>
        /// 订单ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 买家ID。
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// 图书ID。
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 买家姓名。
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// 买家手机号。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 买家收货地址。
        /// </summary>
        public string Address { get; set; }

        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 买家。
        /// </summary>
        public virtual UserEntity Buyer { get; set; }

        /// <summary>
        /// 订单中的图书。
        /// </summary>
        public virtual BookEntity Book { get; set; }

        public static void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).
                ValueGeneratedOnAdd();

            builder.HasOne(entity => entity.Buyer).
                WithMany(entity => entity.Orders).
                HasForeignKey(entity => entity.BuyerId).
                OnDelete(DeleteBehavior.Cascade);

            builder.Property(entity => entity.TimeStamp)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<DateTimeGenerator>();

            builder.HasOne(entity => entity.Book).
                WithMany(entity => entity.Orders).
                HasForeignKey(entity => entity.BookId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
