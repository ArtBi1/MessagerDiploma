using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserService.Security
{
    public class JwtConfiguration
    {
        public string Key { get; init; } = "defaultSecretKey";
        public string Issuer { get; init; } = "defaultIssuer";
        public string Audience { get; init; } = "defaultAudience";

        public JwtConfiguration() { }

        public SymmetricSecurityKey GetSigningKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
    }
}