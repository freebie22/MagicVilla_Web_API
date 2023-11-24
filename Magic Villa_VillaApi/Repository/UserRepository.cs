using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa_VillaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;

        public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("APISettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _context.LocalUsers.FirstOrDefault(u => u.UserName == username);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.LocalUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
            if(user == null)
            {
                return null;
            }

            //If success - generate a JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegistrationRequestModel registrationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Name = registrationRequestDTO.Name,
                Password = registrationRequestDTO.Password,
                Role = registrationRequestDTO.Role
            };
            await _context.LocalUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            user.Password = "";
            return user;

        }
    }
}
