using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2_AboutMe.Models;
using WebApplication2_AboutMe.Services;

namespace WebApplication2_AboutMe.Controllers
{
    public class AboutMeController : Controller
    {
        //private readonly PersonInfoService _personInfoService;
        private readonly SiteContext _siteContext;
        public AboutMeController(SiteContext context)
        {
            _siteContext = context;
        }

        [HttpGet]
        public IActionResult AboutMe()
        {
            Console.WriteLine("Index: ");
            if(_siteContext.PersonInfo.FirstOrDefault() != null)
            {
                ViewData["Info"] = _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault();
			}
            else
            {
                var person = new PersonInfo
                {
                    FirstName = "Vasyl",
                    LastName = "Bihan",
                    Age = 31,
                    Image = null,
                    Skills = new List<Skill>()
                };
                _siteContext.PersonInfo.Add(person);
                _siteContext.SaveChanges();
                ViewData["Info"] = person;
            }

            return View();
        }
        [HttpPost]
        public IActionResult Skills([FromBody] SkillSearchForm form)
        {
            if (form.Query.Length < 1)
            {
                return Json(String.Empty);
            }
            //var a = _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.Where(x => x.Title.ToLower().Contains(form.Query.ToLower())).Select(y => y.Title);
            //return Json(_siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.Where(x => x.Title.ToLower().Contains(form.Query.ToLower())).Select(y => y.Title));
			return Json(_siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.Where(x => x.Title.ToLower().Contains(form.Query.ToLower())));
		}

    }
}
