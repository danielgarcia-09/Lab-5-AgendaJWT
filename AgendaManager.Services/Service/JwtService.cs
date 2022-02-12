using AgendaManager.Bl.Dto;
using AgendaManager.Core.Model;
using AgendaManager.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgendaManager.Services.Service
{
    public interface IJwtService
    {
        Task<string> GetToken(LoginDto user);
    }
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;

        private readonly IUserService _service;

        public JwtService(IOptions<JwtConfig> jwtConfig, IUserService service)
        {
            _jwtConfig = jwtConfig.Value;
            _service = service;
        }

        public async Task<string> GetToken(LoginDto user)
        {
            var isUser = await _service.IsUser(user.Email, user.Password);

            if(isUser != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("UserId", isUser.Id.ToString()),
                    new Claim("UserEmail", isUser.Email.ToString()),
                    new Claim("UserName", isUser.Name.ToString()),
                    new Claim("UserLastName", isUser.LastName.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            return null;
        }
    }
}
