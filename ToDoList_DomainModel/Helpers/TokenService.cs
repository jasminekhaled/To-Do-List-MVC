using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto;
using ToDoList_DomainModel.Models;

namespace ToDoList_DomainModel.Helpers
{
    public static class TokenService
    {
        private static readonly string SecretKey = "KAOpZBEWXsi2dJDtf92IAxFzw/kCePcC1dUJhQ+y/xc=";
        private static readonly string Issuer = "SecureApi";
        public static JwtSecurityToken CreateJwtToken(UserTokenDto dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim("userId", dto.UserId.ToString()),
                new Claim("UserName", dto.userName),
                new Claim(ClaimTypes.Role, dto.Role)
            };

            var token = new JwtSecurityToken(Issuer,
              Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return token;
        }


        public static RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.Now.AddMinutes(120),
                CreatedOn = DateTime.Now
            };
        }

    }
}
