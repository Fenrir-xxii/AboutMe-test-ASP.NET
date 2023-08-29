using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;
using WebApplication2_AboutMe.Services;

namespace WebApplication2_AboutMe.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly PersonInfoService _personInfoService;
        public AboutMeController(PersonInfoService personInfoService)
        {
            _personInfoService = personInfoService;
        }

        [HttpGet]
        public IActionResult AboutMe()
        {
            Console.WriteLine("Index: ");
            ViewData["Info"] = _personInfoService.PersonInfo;

            return View();
        }
        [HttpPost]
        public IActionResult Skills([FromBody] SkillSearchForm form)
        {
            if (form.Query.Length < 1)
            {
                return Json(String.Empty);
            }
            return Json(_personInfoService.PersonInfo.Skills.Select(x => x.Title).Where(x => x.ToLowerInvariant().Contains(form.Query.ToLowerInvariant())));
        }

    }
}
