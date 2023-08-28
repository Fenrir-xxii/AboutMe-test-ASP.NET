using System.ComponentModel.DataAnnotations;

namespace WebApplication2_AboutMe.Models;

public class Skill
{
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public int Level { get; set; }
	public string LogoPath { get; set; } = string.Empty;
}
