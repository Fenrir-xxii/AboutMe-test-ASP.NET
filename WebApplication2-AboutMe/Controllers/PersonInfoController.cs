using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Services;
using WebApplication2_AboutMe.Models;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2_AboutMe.Controllers;

public class PersonInfoController : Controller
{
	//private readonly PersonInfoService _personInfoService;
	private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly SiteContext _siteContext;
    public PersonInfoController(SiteContext siteContext, IWebHostEnvironment hostEnvironment)
	{
        _siteContext = siteContext;
		_webHostEnvironment = hostEnvironment;
	}

	public IActionResult Index()
	{
        if (_siteContext.PersonInfo.FirstOrDefault() != null)
        {
            //var a = _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault();
            return View(_siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault());
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
            return View(person);
        }
       
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
            var nextId = 0;
            if(_siteContext.PersonInfo.FirstOrDefault() !=null)
            {
                if (_siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.Count > 0)
                {
                    nextId = 1 + _siteContext.PersonInfo.FirstOrDefault().Skills.Max(x => x.Id);
                }
            }
            
			var filenameId = nextId.ToString() + "_" + filename;

			using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", filenameId)))
			{
				image.CopyTo(file);
			}
			skill.Image = filenameId;

		}

        _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.Add(skill);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult EditSkill(int id)
    {
        var skill = _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.First(x => x.Id == id);
        return View(skill);
    }
    [HttpPost]
    public IActionResult EditSkill(int id, [FromForm] Skill form, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var skill = _siteContext.PersonInfo.Include(x => x.Skills).FirstOrDefault().Skills.First(x => x.Id == id);

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

        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
	[HttpPost]
	public IActionResult DeleteSkill(int id)
	{
		var skill = _siteContext.PersonInfo.Include(x=> x.Skills).FirstOrDefault().Skills.First(x => x.Id == id);
		if (skill.Image != null)
		{
			System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", skill.Image));
		}
        _siteContext.Remove(skill);
        _siteContext.SaveChanges();
		return RedirectToAction("Index");
	}
	[HttpGet]
	public IActionResult EditPersonalData()
	{
        //return View(_personInfoService.PersonInfo);
        return View(_siteContext.PersonInfo.FirstOrDefault());
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
            if (_siteContext.PersonInfo.FirstOrDefault().Image != null)
            {
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", _siteContext.PersonInfo.FirstOrDefault().Image));
            }
            using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images", filenameId)))
            {
                image.CopyTo(file);
            }
            _siteContext.PersonInfo.FirstOrDefault().Image = filenameId;
        }

        _siteContext.PersonInfo.FirstOrDefault().FirstName = form.FirstName;
        _siteContext.PersonInfo.FirstOrDefault().LastName = form.LastName;
        _siteContext.PersonInfo.FirstOrDefault().Age = form.Age;

        _siteContext.SaveChanges();
		return RedirectToAction("Index");
	}

}
