using Microsoft.AspNetCore.Mvc;
using WebApplication2_AboutMe.Models;
using WebApplication2_AboutMe.Services;

var builder = WebApplication.CreateBuilder(args);
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
app.MapControllerRoute(name: "personInfo_editSkill", pattern: "{controller=PersonInfo}/{action=EditSkill}");

app.UseStaticFiles();

app.Run();



//-----------------------------
//var builder2 = WebApplication.CreateBuilder(args);
//var app2 = builder2.Build();

//app2.MapGet("/", async (context) =>
//{
//    await context.Response.WriteAsync("<h1>Hello World!</h1> <img src=\"images\\image1.jpg\"/> <p>text");
//});

//app2.MapPost("/skills", ([FromBody] PersonInfo person) =>
//{
//    //var skills = new List<string> { "C++", "C#", "JavaScript", "MS SQL Server", "HTML / CSS", "Ado.Net", "Git" };

//    ////var search = context.Request.Body;

//    ////return await context.Response.WriteAsJsonAsync(countries.Where(n => n.ToLowerInvariant().Contains(form.Query.ToLowerInvariant());
//    //Console.WriteLine(person);
//    //return Results.Json(skills.Any(x => person.Skills.Any(y => y.ToLowerInvariant().Contains(x.ToLowerInvariant()))));
//    ////return Results.Json(countries.Where(n => n.ToLowerInvariant().Contains(form.Query.ToLowerInvariant())));


//    var countries = new List<string> { "Ukraine", "United Kingdom", "Poland", "Italy" };

//    //var search = context.Request.Body;

//    //return await context.Response.WriteAsJsonAsync(countries.Where(n => n.ToLowerInvariant().Contains(form.Query.ToLowerInvariant());
//    Console.WriteLine(person.LastName);
//    return Results.Json(countries.Where(n => n.ToLowerInvariant().Contains(person.LastName.ToLowerInvariant())));

//});
//app2.UseStaticFiles();
//app2.Run();