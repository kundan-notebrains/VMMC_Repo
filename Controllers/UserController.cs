using Microsoft.AspNetCore.Mvc;
using VMMC.IServices;
using VMMC.Models.CommanModeles;

namespace VMMC.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IApplicationUser _iapplicationUser;
      
        public UserController(IApplicationUser iapplicationUser)
        {
            _iapplicationUser = iapplicationUser;
            
        }
        #region USERS MODULES
        [HttpPost]
        [Route("Password")]
        public async Task<IActionResult> CreatePassword()
        {          

            if (ModelState.IsValid)
            {
                var generatedPass=await _iapplicationUser.Password();
                return Ok(generatedPass);
            }
           return NotFound();

        }

        [HttpPost]
        [Route("CreateUpdateApplicationUser")]
        public async Task<IActionResult> CreateUpdate([FromBody] Users _applicationUser)
        {

            if (ModelState.IsValid)
            {
                var result = await _iapplicationUser.AddUpdateUser(_applicationUser);
                if (result == true)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("ApplicationUserDetails")]
        public async Task<IActionResult> ApplicationUserGet()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var userLists = await _iapplicationUser.ApplicationUserDetails();
            return Ok(userLists);
        }
        [HttpGet]
        [Route("ApplicationUserEdit")]
        public async Task<IActionResult> ApplicationUserEdit(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var userList = await _iapplicationUser.ApplicationUserEdit(id);
            return Ok(userList);
        }
        [HttpDelete]
        [Route("ApplicationUserDelete")]
        public async Task<IActionResult> ApplicationUserDelete(int id)
        {
            if (id == 0)
                return NotFound();
            var userList = await _iapplicationUser.ApplicationUserDelete(id);
            return Ok(userList);
        }
        [HttpGet]
        [Route("ModulesList")]
        public async Task<IActionResult> ModuleGet()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var moduleLists = await _iapplicationUser.ModulesList();
            return Ok(moduleLists);
        }
        #endregion

       
    }
}
