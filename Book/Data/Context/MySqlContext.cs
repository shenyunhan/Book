using Book.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        { }

        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<BookEntity> Sells { get; set; }

        public virtual DbSet<OrderEntity> Orders { get; set; }

        public virtual DbSet<PostEntity> Posts { get; set; }

        public virtual DbSet<CommentEntity> Comments { get; set; }

        public virtual DbSet<RewardEntity> Rewards { get; set; }

        public virtual DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserEntity.Configure(modelBuilder.Entity<UserEntity>());
            BookEntity.Configure(modelBuilder.Entity<BookEntity>());
            OrderEntity.Configure(modelBuilder.Entity<OrderEntity>());
            PostEntity.Configure(modelBuilder.Entity<PostEntity>());
            CommentEntity.Congifure(modelBuilder.Entity<CommentEntity>());
            RewardEntity.Configure(modelBuilder.Entity<RewardEntity>());
            ShoppingCartEntity.Configure(modelBuilder.Entity<ShoppingCartEntity>());
        }
    }

    public static class MySqlContextExtensions
    {
        public static IServiceCollection AddMySqlContext(this IServiceCollection services, string connectionString)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            return services.AddDbContext<MySqlContext>(options =>
                options.UseMySQL(connectionString));
        }
    }
}
