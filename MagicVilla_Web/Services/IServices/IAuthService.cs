using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Repository
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO);
    }
}
