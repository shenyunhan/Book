using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IUserService
    {
        Task AddUser(string openId, string nickName, string imageURL);

        Task UpdateUser(string openId, string nickName, string imageURL);

        bool FindUser(string openId);

        int GetIdByOpenId(string openId);
    }
}
