using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;

namespace WebApplication2_AboutMe.Controllers
{
    public class AboutMeController : Controller
    {
        [HttpGet]
        public IActionResult AboutMe()
        {
            Console.WriteLine("Index: ");
            //var skills = new List<string> { "C++", "C#", "JavaScript", "MS SQL Server", "HTML / CSS", "Ado.Net", "Git" };
            var person = new PersonInfo
            {
                FirstName = "Vasyl",
                LastName = "Bihan",
                Age = 31,
                //Skills = new List<string> { "C++", "C#", "JavaScript", "SQL", "HTML", "CSS", "Ado.Net", "Git", "jQuery" }
                Skills = new()
            };
            ViewData["Info"] = person;
            //ViewData["Skills"] = person.Skills;
            //ViewData["FirstName"] = person.FirstName;
            //ViewData["LastName"] = person.LastName;
            //ViewData["Age"] = person.Age;


            return View();
        }
        [HttpPost]
        public IActionResult Skills([FromBody] SkillSearchForm form)
        {
            Console.WriteLine("Skills: ");
            var skills = new List<string> { "C++", "C#", "JavaScript", "SQL", "HTML", "CSS", "Ado.Net", "Git", "jQuery" };

            ViewData["Skills"] = skills;
            if (form.Query.Length < 1)
            {
                return Json(String.Empty);
            }
            return Json(skills.Where(x => x.ToLowerInvariant().Contains(form.Query.ToLowerInvariant())));
        }

    }
}
