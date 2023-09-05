using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2_AboutMe.Models;
using WebApplication2_AboutMe.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SiteContext>(options =>
{
    options.UseSqlite("Data Source=site.db");
    //SQLitePCL.Batteries.Init();
});

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.SignIn.RequireConfirmedAccount = false;
	options.SignIn.RequireConfirmedEmail = false;

	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
})
	.AddRoles<IdentityRole<int>>()
	.AddEntityFrameworkStores<SiteContext>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PersonInfoService>(x =>
{
	return new PersonInfoService("personInfo.json");
});
var app = builder.Build();

app.UseAuthentication(); // 1
app.UseAuthorization();  // 2

app.MapControllerRoute(name: "aboutMe", pattern: "{controller=AboutMe}/{action=AboutMe}");
app.MapControllerRoute(name: "skills", pattern: "{controller=AboutMe}/{action=Skills}");
app.MapControllerRoute(name: "personInfo", pattern: "{controller=PersonInfo}/{action=Index}");
app.MapControllerRoute(name: "personInfo_addSkill", pattern: "{controller=PersonInfo}/{action=AddSkill}");
app.MapControllerRoute(name: "personInfo_editSkill", pattern: "{controller=PersonInfo}/{action=EditSkill}/{id}");
app.MapControllerRoute(name: "personInfo_deleteSkill", pattern: "{controller=PersonInfo}/{action=DeleteSkill}/{id}");
app.MapControllerRoute(name: "personInfo_addLogo", pattern: "{controller=PersonInfo}/{action=SkillsLogo}");
app.MapControllerRoute(name: "personInfo_editPersonalData", pattern: "{controller=PersonInfo}/{action=EditPersonalData}");
app.MapControllerRoute(name: "news_addNews", pattern: "{controller=News}/{action=AddNews}");
app.MapControllerRoute(name: "news_editNews", pattern: "{controller=News}/{action=EditNews}/{id}");
app.MapControllerRoute(name: "news_showMore", pattern: "{controller=News}/{action=ShowMore}/{id}");
app.MapControllerRoute(name: "news_showAllNews", pattern: "{controller=News}/{action=AllNews}");
app.MapControllerRoute(name: "news_delete", pattern: "{controller=News}/{action=DeleteNews}/{id}");

app.UseStaticFiles();

app.Run();
