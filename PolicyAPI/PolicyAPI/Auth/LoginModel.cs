using System.ComponentModel.DataAnnotations;

namespace PolicyAPI.Auth
{
    public class LoginModel
    {

        [Required(ErrorMessage = "User Name Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
