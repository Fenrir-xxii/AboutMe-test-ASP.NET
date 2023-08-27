using System.ComponentModel.DataAnnotations;

namespace WebApplication2_AboutMe.Models
{
    public class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        //public List<string> Skills { get; set; }
		public List<Skill> Skills { get; set; }
    }
}
