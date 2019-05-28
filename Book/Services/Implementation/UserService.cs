using Book.Data.Context;
using Book.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly MySqlContext _mySql;

        public UserService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public async Task AddUser(string openId, string nickName, string imageURL)
        {
            await _mySql.Users.
                AddAsync(new UserEntity
                {
                    OpenId = openId,
                    NickName = nickName,
                    ImageURL = imageURL,
                    ExpPoints = 0
                });
            await _mySql.SaveChangesAsync();
        }

        public bool FindUser(string openId)
        {
            return _mySql.Users.
                FirstOrDefault(entity => entity.OpenId == openId) != null;
        }

        public int GetIdByOpenId(string openId)
        {
            var id = _mySql.Users.
                Where(entity => entity.OpenId == openId).
                Select(entity => entity.Id).
                ToArray();

            if (id.Length != 1)
                throw new Exception("OpenId not exists.");

            return id[0];
        }

        public async Task UpdateUser(string openId, string nickName, string imageURL)
        {
            var user = _mySql.Users.
                FirstOrDefault(entity => entity.OpenId == openId);

            if (user == null)
                throw new Exception("User not exists.");

            user.NickName = nickName;
            user.ImageURL = imageURL;
            _mySql.Update(user);
            await _mySql.SaveChangesAsync();
        }
    }

    public static class UserServiceExtensions
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>();
        }
    }
}
