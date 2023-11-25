using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Please, enter your Username"),DisplayName("Логін")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please, enter your Password"), DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
