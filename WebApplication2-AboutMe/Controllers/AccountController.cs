using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;
using WebApplication2_AboutMe.Models.Forms;

namespace WebApplication2_AboutMe.Controllers;

public class AccountController : Controller
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;

	public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}
	[HttpGet]
	public async Task<IActionResult> Login()
	{
		return View(new LoginForm());
	}
	[HttpPost]
	public async Task<IActionResult> Login([FromForm] LoginForm form)
	{
		if (!ModelState.IsValid)
		{
			return View(form);
		}

		var user = await _userManager.FindByEmailAsync(form.Login);
		if (user == null)
		{
			ModelState.AddModelError(nameof(form.Login), "user not exists");
			return View(form);
		}

		var signInResult = await _signInManager.PasswordSignInAsync(user, form.Password, true, false);


		if (!signInResult.Succeeded)
		{
			ModelState.AddModelError(nameof(form.Login), "sign in fail");
			return View(form);
		}
		return Redirect("/");
	}
	[HttpGet]
	public async Task<IActionResult> Register()
	{
		return View(new RegisterForm());
	}
	[HttpPost]
	public async Task<IActionResult> Register([FromForm] RegisterForm form)
	{
		if (!ModelState.IsValid)
		{
			return View(form);
		}

		var user = await _userManager.FindByEmailAsync(form.Login);
		if (user != null)
		{
			ModelState.AddModelError(nameof(form.Login), "Such User already exists");
			return View(form);
		}
		user = new User
		{
			Email = form.Login,
			UserName = form.UserName,
		};

		var registerResult = await _userManager.CreateAsync(user, form.Password);

		if (!registerResult.Succeeded)
		{
			ModelState.AddModelError(nameof(form.Login), "Invalid login or password");
			return View(form);
		}

		var signInResult = await _signInManager.PasswordSignInAsync(user, form.Password, true, false);

		if (!signInResult.Succeeded)
		{
			ModelState.AddModelError(nameof(form.Login), "Invalid password");
			return View(form);
		}
		return Redirect("/");
	}
}
