using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
        //var markerList = _siteContext.MapMarkers.ToList();
        //var markerSerialized = Json(markerList);
        //ViewData["MapMarkers"] = markerSerialized;
        return View();
	}
    [HttpGet]
    public IActionResult AddMarker()
    {
        return View(new MapMarker());
    }
    [HttpPost]
    public IActionResult AddMarker([FromBody] MapMarker marker)
    {
        if (!ModelState.IsValid)
        {
            return View(marker);
        }

        _siteContext.Add(marker);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult GetMarkers()
    {
        return Json(_siteContext.MapMarkers.ToList());
    }
	[HttpGet]
	public IActionResult EditMarker(int id)
    {
        var marker = _siteContext.MapMarkers.First(x => x.Id == id);
        return View(marker);
	}
    [HttpPost]
    public IActionResult EditMarker(int id, [FromForm] MapMarker form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var news = _siteContext.MapMarkers.First(x => x.Id == id);

        news.Title = form.Title;
        news.Latitude = form.Latitude;
        news.Longitude = form.Longitude;

        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }

}
