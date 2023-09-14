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

        //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
        //var currentUser = GetCurrentUser();
        var id = int.Parse(_userManager.GetUserId(User));
        var dbUser = _siteContext.Users.FirstOrDefault(x => x.Id == id);
        task.User = dbUser;

        _siteContext.Add(task);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult GetTasks([FromBody] string date)
    {
        var userId = int.Parse(_userManager.GetUserId(this.User));
        var selectedDate = DateTime.Parse(date);
        return Json(_siteContext.Tasks.Where(x => x.User.Id == userId).Where(x => x.Date == selectedDate).ToList());
    }
    //[HttpGet("/Task/Index/{date?}")]
    //[HttpPost]
    //public async Task<IActionResult> GetTasks([FromBody] string date)
    //{
    //    var selectedDate = DateTime.Parse(date);
    //    ViewData["SelectedDate"] = selectedDate;

    //    var userId = int.Parse(_userManager.GetUserId(this.User));

    //    var tasks = _siteContext.Tasks.Where(x => x.User.Id == userId).Where(x => x.Date == selectedDate).ToList();
    //    ViewData["Tasks"] = tasks;

    //    //return await System.Threading.Tasks.Task.Run(() => View("Index"));
    //    return RedirectToAction("Index");
    //}
    public async Task<User> GetCurrentUser()
    {
        return await _userManager.GetUserAsync(HttpContext.User);
    }

    [HttpPost]
    public IActionResult CompleteTask([FromBody] int taskId)
    {
        var task = _siteContext.Tasks.First(x => x.Id == taskId);

        task.IsCompleted = true;
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RestoreTask([FromBody] int taskId)
    {
        var task = _siteContext.Tasks.First(x => x.Id == taskId);

        task.IsCompleted = false;
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult DeleteTask([FromBody] int taskId)
    {
        var task = _siteContext.Tasks.First(x => x.Id == taskId);

        _siteContext.Remove(task);
        _siteContext.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult DeleteAllCompleted([FromBody] string date)
    {
        var userId = int.Parse(_userManager.GetUserId(this.User));
        var selectedDate = DateTime.Parse(date);
        _siteContext.Tasks.RemoveRange(_siteContext.Tasks.Where(x => x.User.Id == userId).Where(x => x.Date == selectedDate).Where(x => x.IsCompleted==true));
        _siteContext.SaveChanges();

        return RedirectToAction("Index");
    }
}
