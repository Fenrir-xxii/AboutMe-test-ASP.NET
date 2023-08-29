using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Services;
using WebApplication2_AboutMe.Models;
using System.Diagnostics.Metrics;

namespace WebApplication2_AboutMe.Controllers;

public class PersonInfoController : Controller
{
	private readonly PersonInfoService _personInfoService;
	private readonly IWebHostEnvironment _webHostEnvironment;
	public PersonInfoController(PersonInfoService personInfoService, IWebHostEnvironment hostEnvironment)
	{
		_personInfoService = personInfoService;
		_webHostEnvironment = hostEnvironment;
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
    public IActionResult AddSkill([FromForm] Skill skill, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return View(skill);
        }

		if (image != null)
		{
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			var filenameId = _personInfoService.GetNextSkillId().ToString() + "_" + filename;

			using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", filenameId)))
			{
				image.CopyTo(file);
			}
			skill.Image = filenameId;

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
    public IActionResult EditSkill(int id, [FromForm] Skill form, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var skill = _personInfoService.PersonInfo.Skills.First(x => x.Id == id);

		if (image != null)
		{
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			//var filenameId = skill.Id.ToString() + "_" + form.Title + "_" + filename;
			var filenameId = skill.Id.ToString() + "_" + filename;
			if (skill.Image != null)
			{
				System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", skill.Image));
			}
			using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", filenameId)))
			{
				image.CopyTo(file);
			}
			skill.Image = filenameId;
		}

		skill.Title = form.Title;
        skill.Level = form.Level;

        _personInfoService.SaveChanges();
        return RedirectToAction("Index");
    }
	[HttpPost]
	public IActionResult DeleteSkill(int id)
	{
		var skill = _personInfoService.PersonInfo.Skills.First(x => x.Id == id);
		if (skill.Image != null)
		{
			System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", skill.Image));
		}
		_personInfoService.PersonInfo.Skills.Remove(skill);
		_personInfoService.SaveChanges();
		return RedirectToAction("Index");
	}
	[HttpGet]
	public IActionResult EditPersonalData()
	{
		return View(_personInfoService.PersonInfo);
	}
	[HttpPost]
	public IActionResult EditPersonalData([FromForm] PersonInfo form, IFormFile? image)
	{
		if (!ModelState.IsValid)
		{
			return View(form);
		}

        if (image != null)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //var filenameId = skill.Id.ToString() + "_" + form.Title + "_" + filename;
            var filenameId = "user_logo_" + filename;
            if (_personInfoService.PersonInfo.Image != null)
            {
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", _personInfoService.PersonInfo.Image));
            }
            using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", filenameId)))
            {
                image.CopyTo(file);
            }
            _personInfoService.PersonInfo.Image = filenameId;
        }

        _personInfoService.PersonInfo.FirstName = form.FirstName;
		_personInfoService.PersonInfo.LastName = form.LastName;
		_personInfoService.PersonInfo.Age = form.Age;

		_personInfoService.SaveChanges();
		return RedirectToAction("Index");
	}

}
