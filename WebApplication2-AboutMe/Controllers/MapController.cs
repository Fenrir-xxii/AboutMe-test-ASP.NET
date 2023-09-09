using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;

namespace WebApplication2_AboutMe.Controllers;

public class MapController : Controller
{
	private readonly SiteContext _siteContext;
	public MapController(SiteContext siteContext)
	{
		_siteContext = siteContext;
	}
	public IActionResult Index()
	{
		return View();
	}
}
