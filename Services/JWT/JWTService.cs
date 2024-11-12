using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.DTO;
using API.Models;
using API.Services;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.JWT{
    public class JWTService(DbAPIContext context) : IJWTService
    {
        private readonly DbAPIContext _context = context;
        public User? Login(LoginDTO loginDTO)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.mail!.ToLower() == loginDTO.Mail!.ToLower());


            if (user == null) return null;

            // Vérification du mot de passe
            using var hmac = new HMACSHA512(user.PasswordSalt!);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password!));
            
            if (!computedHash.SequenceEqual(user.UserPWD!))
                return null;

            return user;
        }
        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}