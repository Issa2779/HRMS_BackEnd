using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS_BackEnd.Repositories.TokenRepository
{
    public class TokenRepo : IJwtTokenRepo
    {

        private readonly IConfiguration _configuration;

        public TokenRepo(IConfiguration configuration) { 
        
           _configuration = configuration;
        
        }
        public string CreateJWTToken(IdentityUser user)
        {
            //Claims

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
