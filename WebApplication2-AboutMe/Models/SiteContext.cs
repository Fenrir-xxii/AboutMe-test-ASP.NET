using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2_AboutMe.Models;

public class SiteContext: DbContext
{
    public SiteContext(DbContextOptions options) : base(options) { }
    public virtual DbSet<PersonInfo> PersonInfo { get; set; } = null!;
    public virtual DbSet<Skill> Skills { get; set; } = null!;

}
