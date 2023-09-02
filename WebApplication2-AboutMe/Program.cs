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


builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PersonInfoService>(x =>
{
	return new PersonInfoService("personInfo.json");
});
var app = builder.Build();

app.MapControllerRoute(name: "aboutMe", pattern: "{controller=AboutMe}/{action=AboutMe}");
app.MapControllerRoute(name: "skills", pattern: "{controller=AboutMe}/{action=Skills}");
app.MapControllerRoute(name: "personInfo", pattern: "{controller=PersonInfo}/{action=Index}");
app.MapControllerRoute(name: "personInfo_addSkill", pattern: "{controller=PersonInfo}/{action=AddSkill}");
app.MapControllerRoute(name: "personInfo_editSkill", pattern: "{controller=PersonInfo}/{action=EditSkill}/{id}");
app.MapControllerRoute(name: "personInfo_deleteSkill", pattern: "{controller=PersonInfo}/{action=DeleteSkill}/{id}");
app.MapControllerRoute(name: "personInfo_addLogo", pattern: "{controller=PersonInfo}/{action=SkillsLogo}");
app.MapControllerRoute(name: "personInfo_editPersonalData", pattern: "{controller=PersonInfo}/{action=EditPersonalData}");
app.UseStaticFiles();

app.Run();
