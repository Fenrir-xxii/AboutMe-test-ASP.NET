using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2_AboutMe.Models;

namespace WebApplication2_AboutMe.Controllers;

public class NewsController : Controller
{
	private readonly SiteContext _siteContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public NewsController(SiteContext context, IWebHostEnvironment hostEnvironment)
	{
		_siteContext = context;
        _webHostEnvironment = hostEnvironment;
	}
	public IActionResult Index()
	{
        //var news = new List<NewsItem> {
        //	new NewsItem
        //	{
        //		Title = "news#1",
        //		FullDescription = "fjkljdnfkgsjdfng;klsdjfng" ,
        //		CreatedAt = DateTime.Now,
        //	},
        //	new NewsItem
        //	{
        //		Title = "news#2",
        //		FullDescription = "ergrthtyjhgjkhjkghjkghj",
        //		CreatedAt = new DateTime(2023, 01, 01)
        //          },
        //      };
        var news = _siteContext.News.ToList();
        ViewData["News"] = news;
        return View();
	}
    [HttpGet]
    public IActionResult AddNews()
    {
        return View(new NewsItem());
    }
    [HttpPost]
    public IActionResult AddNews([FromForm] NewsItem news, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return View(news);
        }

        if (image != null)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var nextId = 0;
            if (_siteContext.News.ToList().Count > 0)
            {
                nextId = 1 + _siteContext.News.ToList().Max(x => x.Id);
            }

            var filenameId = nextId.ToString() + "_" + filename;

            using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images/news/", filenameId)))
            {
                image.CopyTo(file);
            }
            news.Image = filenameId;

        }

        _siteContext.Add(news);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
	[HttpGet]
	public IActionResult EditNews(int id)
	{
		var news = _siteContext.News.First(x => x.Id == id);
		return View(news);
	}
	[HttpPost]
	public IActionResult EditNews(int id, [FromForm] NewsItem form, IFormFile? image)
	{
		if (!ModelState.IsValid)
		{
			return View(form);
		}

		var news = _siteContext.News.First(x => x.Id == id);

		if (image != null)
		{
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			//var filenameId = skill.Id.ToString() + "_" + form.Title + "_" + filename;
			var filenameId = news.Id.ToString() + "_" + filename;
			if (news.Image != null)
			{
				System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images/news/", news.Image));
			}
			using (var file = System.IO.File.OpenWrite(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images/news", filenameId)))
			{
				image.CopyTo(file);
			}
			news.Image = filenameId;
		}

		news.Title = form.Title;
		news.CreatedAt = form.CreatedAt;
        news.FullDescription = form.FullDescription;

		_siteContext.SaveChanges();
		return RedirectToAction("Index");
	}
    [HttpGet]
    public IActionResult ShowMore(int id)
    {
        var news = _siteContext.News.First(x => x.Id == id);
        return View(news);
    }
    [HttpGet]
    public IActionResult AllNews()
    {
        var news = _siteContext.News.OrderByDescending(x => x.CreatedAt).Take(6).ToList();
        ViewData["News"] = news;
        return View(news);
    }
    [HttpPost]
    public IActionResult DeleteNews(int id)
    {
        var news = _siteContext.News.First(x => x.Id == id);
        if (news.Image != null)
        {
            System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images/news", news.Image));
        }
        _siteContext.Remove(news);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
}
