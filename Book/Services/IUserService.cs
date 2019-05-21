namespace Book.Services
{
    public interface IUserService
    {
        void AddUser(string openId, string nickName, string imageURL);

        void UpdateUser(string openId, string nickName, string imageURL);

        bool FindUser(string openId);

        int GetIdByOpenId(string openId);
    }
}
