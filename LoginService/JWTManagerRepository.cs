﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VMMC.IServices.ILogin;
using VMMC.Models.CommanModeles;

namespace VMMC.LoginService
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly  IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration) 
        {
            this.iconfiguration = iconfiguration;
        }
        public Tokens? GenerateRefreshToken(string userName, string firstName)
        {
            return GenerateJWTTokens(userName, firstName);
        }

        public Tokens? GenerateToken(string userName, string firstName)
        {
            return GenerateJWTTokens(userName, firstName);
        }
        public Tokens? GenerateJWTTokens(string userName, string clientSchema)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.GivenName, clientSchema)
                    }),
                    Expires = DateTime.Now.AddMinutes(GlobalsAuthProvider._accessTokenTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens { Access_Token = tokenHandler.WriteToken(token), Refresh_Token = refreshToken };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }
    }
}
