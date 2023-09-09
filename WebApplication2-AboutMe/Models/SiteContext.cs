using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2_AboutMe.Models;

public class SiteContext : IdentityDbContext<User, IdentityRole<int>, int>
{
	public SiteContext(DbContextOptions options) : base(options) { }
	public virtual DbSet<PersonInfo> PersonInfo { get; set; } = null!;
	public virtual DbSet<Skill> Skills { get; set; } = null!;
	public virtual DbSet<NewsItem> News { get; set; } = null!;
	public override DbSet<User> Users { get; set; } = null!;
	public virtual DbSet<MapMarker> MapMarkers { get; set; } = null!;

}



//public class SiteContext: DbContext
//{
//    public SiteContext(DbContextOptions options) : base(options) { }
//    public virtual DbSet<PersonInfo> PersonInfo { get; set; } = null!;
//    public virtual DbSet<Skill> Skills { get; set; } = null!;
//    public virtual DbSet<NewsItem> News { get; set; } = null!;

//}
