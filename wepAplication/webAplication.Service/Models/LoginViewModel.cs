using System.ComponentModel.DataAnnotations;

namespace webAplication.Service.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Fill Login field")]
    [MaxLength(20, ErrorMessage = "Login must have length under 20 symbols")]
    [MinLength(4, ErrorMessage = "Login must have length more then 4 symbols")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Fill Password field")]
    [Display(Name = "Password")]
    public string Password { get; set; }
}