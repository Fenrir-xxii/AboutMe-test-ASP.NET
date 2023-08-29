using System.Diagnostics.Metrics;
using System.Text.Json;
using WebApplication2_AboutMe.Models;

namespace WebApplication2_AboutMe.Services;

public class PersonInfoService
{
	public PersonInfoService(string file)
	{
		_file = file;
		Load();
	}
	private string _file;
	public PersonInfo PersonInfo { get; private set; }
	private void Load()
	{
		if (!File.Exists(_file))
		{
            //CreateFile();
            PersonInfo = new PersonInfo
            {
                Skills = new List<Skill>()
            };
        }
		else
		{
			PersonInfo = JsonSerializer.Deserialize<PersonInfo>(File.ReadAllText(_file));
		}
	}
	public void SaveChanges()
	{
		File.WriteAllText(_file, JsonSerializer.Serialize(PersonInfo));
	}
	public void Add(Skill skill)
	{
		skill.Id = (PersonInfo.Skills.Count == 0) ? 0 : 1 + PersonInfo.Skills.Max(skill => skill.Id);
		
		// check if skill already exists
		PersonInfo.Skills.Add(skill);
	}
    public int GetNextSkillId()
    {
        return (PersonInfo.Skills.Count == 0) ? 0 : 1 + PersonInfo.Skills.Max(skill => skill.Id);
	}
    //public string LogoPath { get; set; } = String.Empty;
	private void CreateFile()
	{
        var person = new PersonInfo
        {
            LastName = "Bihan",
			FirstName = "Vasyl",
			Age = 31,
			Skills = new List<Skill>
			{
				new Skill
				{
					Id= 0,
					Title = "C++",
					Level = 70,
					//LogoPath = "/local/storage/img/cpp.png"
                },
                new Skill
                {
                    Id= 1,
                    Title = "C#",
                    Level = 75,
                    //LogoPath = "/local/storage/img/cs.png"
                },
                new Skill
                {
                    Id= 2,
                    Title = "JavaScript",
                    Level = 73,
                    //LogoPath = "/local/storage/img/js.png"
                },
                new Skill
                {
                    Id= 3,
                    Title = "SQL",
                    Level = 80,
                    //LogoPath = "/local/storage/img/sql.jpg"
                },
                new Skill
                {
                    Id= 4,
                    Title = "HTML",
                    Level = 68,
                    //LogoPath = "/local/storage/img/html.png"
                },
                new Skill
                {
                    Id= 5,
                    Title = "CSS",
                    Level = 72,
                    //LogoPath = "/local/storage/img/css.png"
                },
                new Skill
                {
                    Id= 5,
                    Title = "Ado.net",
                    Level = 63,
                    //LogoPath = "/local/storage/img/ado-net.jpg"
                },
                new Skill
                {
                    Id= 5,
                    Title = "Git",
                    Level = 77,
                    //LogoPath = "/local/storage/img/git.png"
                },
                new Skill
                {
                    Id= 5,
                    Title = "jQuery",
                    Level = 73,
                    //LogoPath = "/local/storage/img/jquery.png"
                }

            }
        };
        File.WriteAllText("personInfo.json", JsonSerializer.Serialize(person));
    }
	
}
