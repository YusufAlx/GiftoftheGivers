using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace GiftOfTheGivers.Web.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) { public DbSet<Donation> Donations => Set<Donation>(); public DbSet<Volunteer> Volunteers => Set<Volunteer>(); public DbSet<ProjectUpdate> ProjectUpdates => Set<ProjectUpdate>(); }
