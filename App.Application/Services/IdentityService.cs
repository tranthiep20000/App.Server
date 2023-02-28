using App.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of IdentityService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;

        public IdentityService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        }

        /// <summary>
        /// TokenHandler
        /// CreatedBy: ThiepTT(27/02/2023)
        /// </summary>
        public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// CreateSecurityToken
        /// </summary>
        /// <param name="identity">ClaimsIdentity</param>
        /// <returns>SecurityToken</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);

            return TokenHandler.CreateToken(tokenDescriptor);
        }

        /// <summary>
        /// WriteToken
        /// </summary>
        /// <param name="token">SecurityToken</param>
        /// <returns>Token</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public string WriteToken(SecurityToken token)
        {
            return TokenHandler.WriteToken(token);
        }

        /// <summary>
        /// GetTokenDescriptor
        /// </summary>
        /// <param name="identity">ClaimsIdentity</param>
        /// <returns>SecurityTokenDescriptor</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(23),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}