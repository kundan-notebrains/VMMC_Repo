using VMMC.Models;
using VMMC.Models.CommanModeles;

namespace VMMC.IServices
{
    public interface IApplicationUser
    {
        #region USER MODULES
        Task<bool> AddUpdateUser(Users _user);
        Task<List<ApplicationUser>> ApplicationUserDetails();
        Task<List<ApplicationUser>> ApplicationUserEdit(int id);
        Task<bool> ApplicationUserDelete(int id);
        Task<List<Module>> ModulesList();
        Task<UserID> Password();
        #endregion
        #region LOGIN SYSTEM
        #endregion
    }
}
