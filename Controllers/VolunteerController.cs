using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace GiftOfTheGivers.Web.Controllers;
public class VolunteerController(ApplicationDbContext db) : Controller { [HttpGet] public IActionResult Register() => View(new Volunteer()); [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Register(Volunteer volunteer) { if (!ModelState.IsValid) return View(volunteer); db.Volunteers.Add(volunteer); await db.SaveChangesAsync(); return View("Confirmation", volunteer); } }
