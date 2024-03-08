using Microsoft.EntityFrameworkCore;
using VMMC.CommonPage;
using VMMC.IServices;
using VMMC.IServices.ILogin;
using VMMC.Models;
using VMMC.Models.CommanModeles;

namespace VMMC.Services
{
    public class ApplicationService : IApplicationUser
    {
        private readonly VmmcContext _context;
        private readonly IHashingAndSalting _hashAndVerify;

        public ApplicationService(VmmcContext context, IHashingAndSalting hashingAndSalting)
        {
            _context = context;
            _hashAndVerify = hashingAndSalting;
        }
        #region USER MODULES
        public async Task<bool> AddUpdateUser(Users _user)
        {
            bool result;
            if (_user.Userid > 0)
            {
                result = await UpdateUSer(_user);
                return result;
            }
            else
            {
                result = await CreateUSer(_user);
                return result;
            }

        }
        public async Task<bool> CreateUSer(Users _user)
        {
            var hashValue = _hashAndVerify.HashingPassword(_user.Userpassword!, out var saltValue);
            
            string parameter = $"usp_ApplicationUser_create_update @id='{_user.Userid}', @name='{_user.Username}',@surname='{_user.Usersurname}', @emailid='{_user.Useremailid}'," +
               $" @phonenumber='{_user.Userphonenumber}' ,@userid='{_user.Useruserid}',@accesslevelid='{_user.Useraccesslevelid}' ,@employeeno='{_user.Useremployeeno}', @dateofbirth='{_user.Userdateofbirth}', @officelocation='{_user.Userofficelocation}', @Password='{hashValue}'";
            await _context.Database.ExecuteSqlRawAsync(parameter);

            var maxId = await _context.ApplicationUsers.MaxAsync(u => (int?)u.Id) ?? 0;


            foreach (var item in _user.Usermoduleid!)
            {
                foreach (var roleItem in _user.Userroleid!)
                {
                    foreach (var roleid in roleItem.Value!)
                    {
                        if (roleItem.Key == item.Key)
                        {
                            string moduleRoles = $"usp_usermodulerole_insert @userid='{maxId}',@moduleid='{item.Value}',@accessroleid='{roleid}'";
                            await _context.Database.ExecuteSqlRawAsync(moduleRoles);
                        }

                    }

                }

            }
            return true;
        }
        public async Task<bool> UpdateUSer(Users _user)
        {
            await deleteModule(_user);

            string parameter = $"usp_ApplicationUser_create_update @id='{_user.Userid}', @name='{_user.Username}',@surname='{_user.Usersurname}', @emailid='{_user.Useremailid}'," +
               $" @phonenumber='{_user.Userphonenumber}' ,@userid='{_user.Useruserid}', @accesslevelid='{_user.Useraccesslevelid}',@employeeno='{_user.Useremployeeno}', @dateofbirth='{_user.Userdateofbirth}', @officelocation='{_user.Userofficelocation}', @Password='{_user.Userpassword}'";
            await _context.Database.ExecuteSqlRawAsync(parameter);


            foreach (var item in _user.Usermoduleid!)
            {
                foreach (var roleItem in _user.Userroleid!)
                {
                    foreach (var roleid in roleItem.Value!)
                    {
                        if (roleItem.Key == item.Key)
                        {
                            string moduleRoles = $"usp_usermodulerole_insert @userid='{_user.Userid}',@moduleid='{item.Value}',@accessroleid='{roleid}'";
                            await _context.Database.ExecuteSqlRawAsync(moduleRoles);
                        }

                    }

                }

            }
            return true;
        }

        public async Task<bool> deleteModule(Users _user)
        {
            var data = await _context.Database.ExecuteSqlRawAsync($"usp_remove_module '{_user.Userid}'");
            if (data == 0)
                return false;
            return true;

            
        }
      
        public async Task<List<ApplicationUser>> ApplicationUserDetails()
        {

            var data = await _context.ApplicationUsers.FromSqlRaw($"exec usp_application_Details").ToListAsync();

            return data!;
        }
        public async Task<List<ApplicationUser>> ApplicationUserEdit(int id)
        {                   
            var data = await _context.ApplicationUsers.FromSqlRaw($"exec usp_applicationuser_edit '{id}'").ToListAsync();            
            return data!;
        }
        public async Task<bool> ApplicationUserDelete(int id)
        {
            var data = await _context.Database.ExecuteSqlRawAsync($"usp_applicationuser_Delete '{id}'");
            if (data == 0)
                return false;
            return true;
        }
        public async Task<List<Module>> ModulesList()
        {
            var list = await _context.Modules.Where(x => x.Isdeleted == false && x.Status == true).ToListAsync();
            if (list == null)
                return list!;
            return list;
        }
        public async Task<UserID> Password()
        {
            int length = 16;
            int cc = 4;
           
            var password = GeneratedPassword.Generate(length, cc);
            var userId = await Task.Run(() => GenerateUserId());
            var passwordUser = new UserID
            {
                password=password,
                usersid= userId
            };
            
            return passwordUser!;
        }



        public string GenerateUserId()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return $"U{randomNumber:D4}";
        }
        #endregion
       
    }
}
