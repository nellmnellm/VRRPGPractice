using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Jwt
{
    public static class JwtUtils
    {
        internal const string SecretKey = " ";
        private static readonly byte[] KeyBytes = Encoding.UTF8.GetBytes(SecretKey);
        internal static readonly SymmetricSecurityKey SymKey = new SymmetricSecurityKey(KeyBytes);

        internal const string Issuer = "AuthService";
        internal const string Audience = "GameClients";

        /// <summary>
        /// 토큰 생성
        /// </summary>
        public static string Generate(string userId, string sessionId, TimeSpan lifetime)
        {
            var creds = new SigningCredentials(SymKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), // 사용자 식별
                new Claim("session_id", sessionId), // 세션 식별
            };

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now + lifetime,
                    signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 토큰 검증
        /// </summary>
        public static ClaimsPrincipal Validate(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SymKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            var handler = new JwtSecurityTokenHandler();
            return handler.ValidateToken(token, parameters, out _);
        }
    }

}
