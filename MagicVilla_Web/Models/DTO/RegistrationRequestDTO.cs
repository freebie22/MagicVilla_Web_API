using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class RegistrationRequestDTO
    {
        [Required(ErrorMessage = "Please, enter your Username"), DisplayName("Логін")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please, enter your Name"), DisplayName("Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter your Password"), DisplayName("Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please, enter your Role"), DisplayName("Роль")]
        public string Role { get; set; }
    }
}
