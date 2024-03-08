using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMMC.IServices.ILogin;
using VMMC.Models.CommanModeles;

namespace VMMC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountManagementController : Controller
    {
        private readonly IUserServiceRepository _iuserServiceRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        public AccountManagementController(IUserServiceRepository userServiceRepository, IJWTManagerRepository jWTManagerRepository)
        {
            _iuserServiceRepository = userServiceRepository;
            _jWTManagerRepository = jWTManagerRepository;
        }
        #region LOGIN SYSTEM
        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LoginByIdAndPasskey([FromBody] LoginModel usersdata)
        {

            var validUser = await _iuserServiceRepository.IsValidUserAsync(usersdata);

            if (!validUser)
            {
                return new JsonResult("Invalid Credentials");
            }

            var token = _jWTManagerRepository.GenerateToken(usersdata.UserId, usersdata.Password!);

            if (token == null)
            {
                return new JsonResult("Invalid Attempt! Token Not Generated");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                TokenKey = token.Refresh_Token,
                UserId = usersdata.UserId,

            };

            await _iuserServiceRepository.UpdateRefreshTokens(obj);


            token.StatusCode = HttpStatusCode.Created;
            token.Message = "Token Generated";
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(Tokens token)
        {

            var principal = _jWTManagerRepository.GetPrincipalFromExpiredToken(token.Access_Token!);
            var username = principal.Identity?.Name;


            //retrieve the saved refresh token from database
            var savedRefreshToken = await _iuserServiceRepository.GetSavedRefreshTokens(username!, token.Refresh_Token!);

            if (savedRefreshToken.TokenKey != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jWTManagerRepository.GenerateRefreshToken(username!, token.name!);

            //set name in token
            newJwtToken!.name = token.name;

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                TokenKey = newJwtToken.Refresh_Token,
                UserId = username!,
                name = token.name
            };

            bool? response = await _iuserServiceRepository.UpdateRefreshTokens(obj);

            if (!(bool)response)
            {
                return Unauthorized("Update Error");
            }

            return Ok(newJwtToken);
        }
        #endregion
    }
}
