using System.ComponentModel.DataAnnotations;

namespace WebUI.Dtos.IdentityDtos
{
    public class LoginDto
    {
        [Required, DataType(DataType.Text), Display(Name = "Username")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
    }
}
