using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data.Entities
{
    public class ShoppingCartEntity
    {
        /// <summary>
        /// 购物车记录的用户ID。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 购物车中的图书ID。
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 购买数量。
        /// </summary>
        public int Number { get; set; }

        public static void Configure(EntityTypeBuilder<ShoppingCartEntity> builder)
        {
            builder.ToTable("ShoppingCart");

            builder.HasKey(entity => new { entity.UserId, entity.BookId });
        }
    }
}
