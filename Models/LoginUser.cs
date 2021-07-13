using System.ComponentModel.DataAnnotations;

namespace HobbyExam.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name = "Login UserName")]
        public string LoginUserName { get; set; }
        [Required]
        [Display(Name = "Login Password")]
        [MinLength(8, ErrorMessage = "Password must be 8 Characters or more!")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}