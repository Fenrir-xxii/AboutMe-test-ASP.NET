using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Services;
using WebApplication2_AboutMe.Models;
using System.Diagnostics.Metrics;

namespace WebApplication2_AboutMe.Controllers;

public class PersonInfoController : Controller
{
	private readonly PersonInfoService _personInfoService;
	//private string _tempPath;
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
    //[HttpPost]
    //public IActionResult AddSkill([FromForm] Skill skill)
    //{
    //	if (_tempPath !=null &&  _tempPath.Length > 0)
    //	{
    //		skill.LogoPath = _tempPath;
    //		_tempPath = String.Empty;
    //	}
    //	if (!ModelState.IsValid)
    //	{
    //		return View(skill);
    //	}

    //	_personInfoService.Add(skill);
    //	_personInfoService.SaveChanges();
    //	return RedirectToAction("Index");
    //}
    [HttpPost]
    public IActionResult AddSkill([FromForm] Skill skill)
    {
        var dir = Directory.GetCurrentDirectory() + @"\wwwroot" + Repo.SelectedLogoPath;

        if (System.IO.File.Exists(dir))
        {
            skill.LogoPath = Repo.SelectedLogoPath;
            Repo.SelectedLogoPath = String.Empty;
        }
        else
        {
            skill.LogoPath = Repo.DefaultLogoPath;
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
	public IActionResult DeleteSkill(int id)
	{
		var skill = _personInfoService.PersonInfo.Skills.First(x => x.Id == id);
		_personInfoService.PersonInfo.Skills.Remove(skill);
		_personInfoService.SaveChanges();
		return RedirectToAction("Index");
	}
	[HttpGet]
	public IActionResult EditPersonalData()
	{
		//var person = _personInfoService.PersonInfo;
		return View(_personInfoService.PersonInfo);
	}
	[HttpPost]
	public IActionResult EditPersonalData([FromForm] PersonInfo form)
	{
        //form.Skills = _personInfoService.PersonInfo.Skills;

		if (!ModelState.IsValid)
		{
			return View(form);
		}

		//var person = _personInfoService.PersonInfo;

		_personInfoService.PersonInfo.FirstName = form.FirstName;
		_personInfoService.PersonInfo.LastName = form.LastName;
		_personInfoService.PersonInfo.Age = form.Age;

		_personInfoService.SaveChanges();
		return RedirectToAction("Index");
	}

	[HttpPost]
	public void SkillsLogo([FromBody] SkillSearchForm form)
	{
		if (form.Query.Length > 0)
		{
            Repo.SelectedLogoPath = form.Query;
            //_personInfoService.LogoPath = form.Query;
		}
	}
}
