using System.ComponentModel.DataAnnotations;

namespace WebApplication2_AboutMe.Models.Forms;

public class LoginForm
{
	[Required]
	[EmailAddress]
	public string Login { get; set; }
	[Required]
	public string Password { get; set; }
}
