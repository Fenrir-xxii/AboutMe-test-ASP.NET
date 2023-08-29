using System.ComponentModel.DataAnnotations;

namespace WebApplication2_AboutMe.Models
{
    public class PersonInfo
    {
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public int Age { get; set; }
        //public List<string> Skills { get; set; }
		public List<Skill> Skills { get; set; } = new List<Skill>();
		public string? Image { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public string ImageUrl => "/uploads/images/" + Image;
	}
}
