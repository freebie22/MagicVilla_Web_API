using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;

namespace Magic_Villa_VillaApi.Repository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
