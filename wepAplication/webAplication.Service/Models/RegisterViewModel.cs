using System.ComponentModel.DataAnnotations;
using webAplication.Domain;
using webAplication.Domain.Interfaces;

namespace webAplication.Service.Models;


public class RegisterViewModel
{
    [Required(ErrorMessage = "Fill Login field")]
    [MaxLength(20, ErrorMessage = "Login must have length under 20 symbols")]
    [MinLength(4, ErrorMessage = "Login must have length more then 4 symbols")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Fill Password field")]
    [MaxLength(20, ErrorMessage = "Password must have length under 20 symbols")]
    [MinLength(6, ErrorMessage = "Password must have length more then 6 symbols")]
    public string Password { get; set; }

    public string role = "SchoolKidRole";
}