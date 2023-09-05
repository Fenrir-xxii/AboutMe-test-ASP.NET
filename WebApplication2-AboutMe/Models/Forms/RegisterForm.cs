using System.ComponentModel.DataAnnotations;

namespace WebApplication2_AboutMe.Models.Forms;

public class RegisterForm
{
	[Required]
	public string UserName { get; set; }
	[Required]
	[EmailAddress]
	public string Login { get; set; }
	[Required]
	public string Password { get; set; }
}
