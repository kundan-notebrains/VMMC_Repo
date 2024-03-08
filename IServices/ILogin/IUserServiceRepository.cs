using VMMC.Models;
using VMMC.Models.CommanModeles;

namespace VMMC.IServices.ILogin
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(LoginModel users);
        Task<UserRefreshTokens> GetSavedRefreshTokens(string username, string refreshtoken);
        Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user);
        Task<bool?> UpdateRefreshTokens(UserRefreshTokens user);
      // Task<bool> DeleteUserRefreshTokens(string username, string b);
    }
}
