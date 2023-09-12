using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;
using Task = WebApplication2_AboutMe.Models.Task;

namespace WebApplication2_AboutMe.Controllers;
[Authorize]
public class TaskController : Controller
{
    private readonly SiteContext _siteContext;
    private readonly UserManager<User> _userManager;
    private readonly int _userId;   
    public TaskController(SiteContext siteContext, UserManager<User> userManager)
    {
        _siteContext = siteContext;
        _userManager = userManager;
        
    }

    public IActionResult Index()
	{
		return View();
	}
    [HttpGet]
    public IActionResult AddTask()
    {
        return View(new Task());
    }
    [HttpPost]
    public IActionResult AddTask([FromBody] Task task)
    {
        ModelState.Remove("User");
        if (!ModelState.IsValid)
        {
            return View(task);
        }

        System.Security.Claims.ClaimsPrincipal currentUser = this.User;
        var id = int.Parse(_userManager.GetUserId(User));
        var dbUser = _siteContext.Users.FirstOrDefault(x => x.Id == _userId);
        task.User = dbUser;

        _siteContext.Add(task);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult GetTasks(string date)
    {
        var userId = int.Parse(_userManager.GetUserId(this.User));
        var selectedDate = DateTime.Parse(date);
        return Json(_siteContext.Tasks.Where(x => x.User.Id == userId).Where(x => x.Date == selectedDate).ToList());
    }
}
