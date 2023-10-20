using CryptoTracker_backend.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CryptoTracker_backend.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string CreateToken(UserCredentials userInfo)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Uri, $"{userInfo.User.UserDataId}"),
                new Claim(ClaimTypes.Role, userInfo.Role),
            };

            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool IsUserToken(int numUser, ClaimsPrincipal UserClaim)
        {
            var userIdToken = UserClaim.FindFirst(ClaimTypes.Uri)?.Value;

            return numUser != Convert.ToInt32(userIdToken);

        }
    }
}
