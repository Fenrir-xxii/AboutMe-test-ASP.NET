using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Services;
using WebApplication2_AboutMe.Models;
using System.Diagnostics.Metrics;

namespace WebApplication2_AboutMe.Controllers;

public class PersonInfoController : Controller
{
	private readonly PersonInfoService _personInfoService;
	private string _tempPath;
	public PersonInfoController(PersonInfoService personInfoService)
	{
		_personInfoService = personInfoService;
	}

	public IActionResult Index()
	{
		return View(_personInfoService.PersonInfo);
	}
	[HttpGet]
	public IActionResult AddSkill()
	{
		return View(new Skill());
	}
	[HttpPost]
	public IActionResult AddSkill([FromForm] Skill skill)
	{
		if (_tempPath !=null &&  _tempPath.Length > 0)
		{
			skill.LogoPath = _tempPath;
			_tempPath = String.Empty;
		}
		if (!ModelState.IsValid)
		{
			return View(skill);
		}
		
		_personInfoService.Add(skill);
		_personInfoService.SaveChanges();
		return RedirectToAction("Index");
	}
    [HttpGet]
    public IActionResult EditSkill(int id)
    {
        var skill = _personInfoService.PersonInfo.Skills.First(x => x.Id == id);
        return View(skill);
    }
    [HttpPost]
    public IActionResult EditSkill(int id, [FromForm] Skill form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var skill = _personInfoService.PersonInfo.Skills.First(x => x.Id == id);

        skill.Title = form.Title;
        skill.Level = form.Level;

        _personInfoService.SaveChanges();
        return RedirectToAction("Index");
    }
	[HttpPost]
	public void SkillsLogo([FromBody] SkillSearchForm form)
	{
		if (form.Query.Length > 0)
		{
			_tempPath = form.Query;
		}
	}
}
