using System.Security.Claims;
using VMMC.Models.CommanModeles;

namespace VMMC.IServices.ILogin
{
    public interface IJWTManagerRepository
    {
        Tokens? GenerateToken(string userName, string firstName);
        Tokens? GenerateRefreshToken(string userName, string firstName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
