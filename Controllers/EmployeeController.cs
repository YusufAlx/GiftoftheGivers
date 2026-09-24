using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GiftOfTheGivers.Web.Controllers;
[Authorize(Roles = "Employee")]
public class EmployeeController(ApplicationDbContext db) : Controller { public async Task<IActionResult> Dashboard() { ViewBag.Volunteers = await db.Volunteers.OrderByDescending(x => x.RegistrationDate).ToListAsync(); ViewBag.Updates = await db.ProjectUpdates.OrderByDescending(x => x.PostedDate).ToListAsync(); return View(); } [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> PostUpdate(ProjectUpdate update) { if (ModelState.IsValid) { update.PostedBy = User.Identity?.Name ?? "Employee"; db.ProjectUpdates.Add(update); await db.SaveChangesAsync(); } return RedirectToAction(nameof(Dashboard)); } }
