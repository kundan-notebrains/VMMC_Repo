using Microsoft.EntityFrameworkCore;
using VMMC.IServices.ILogin;
using VMMC.Models;
using VMMC.Models.CommanModeles;

namespace VMMC.LoginService
{
    public class LoginManagementService : IUserServiceRepository
    {
        private readonly IHashingAndSalting _hashAndVerify;
        private readonly VmmcContext _context;
     
        public LoginManagementService(VmmcContext vmmcContext, IHashingAndSalting hashingAndSalting)
        {
            _context = vmmcContext;
            _hashAndVerify = hashingAndSalting;
           
        }
        public async Task<bool> IsValidUserAsync(LoginModel _users)
        {

            var hashValue = _hashAndVerify.HashingPassword(_users.Password!, out var saltValue);

            var response = await _context.ApplicationUsers.Where(x => x.Userid == _users.UserId && x.Password == hashValue && x.Status == _users.isactive && x.Isdeleted == _users.isdeleted).FirstOrDefaultAsync();
            if (response == null)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public async Task<UserRefreshTokens> GetSavedRefreshTokens(string username, string? refreshtoken)
        {
            var refreshGetToken = await _context.ApplicationUsers.Where(x => x.Userid == username && x.Token == refreshtoken && x.Status == true && x.Isdeleted == false).FirstOrDefaultAsync();

            if (refreshGetToken != null)
            {
                var result = new UserRefreshTokens
                {
                    TokenKey = refreshGetToken.Token
                };
                return result;
            }

         
            return null;

        }
        public async Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens _user)
        {
            var refreshUser = new ApplicationUser
            {
                Userid = _user.UserId,
                Emailid = _user.Emailaddress,
                Token = _user.TokenKey,
                Isdeleted = false,
                Status = _user.IsActive

            };
            _context.ApplicationUsers.Add(refreshUser);
            await _context.SaveChangesAsync();
            return new UserRefreshTokens
            {
                UserId = refreshUser.Userid,
                Emailaddress = refreshUser.Emailid,
                IsActive = refreshUser.Status

            };
        }

        public async Task<bool?> UpdateRefreshTokens(UserRefreshTokens _user)
        {
            
            var data = await _context.ApplicationUsers.Where(x => x.Userid == _user.UserId && x.Status == _user.IsActive && x.Isdeleted==false).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Token = _user.TokenKey;
                _context.ApplicationUsers.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        
    }
}
