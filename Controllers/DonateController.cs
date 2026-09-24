using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace GiftOfTheGivers.Web.Controllers;
public class DonateController(ApplicationDbContext db) : Controller { [HttpGet] public IActionResult Index() => View(new Donation()); [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Index(Donation donation) { if (!ModelState.IsValid) return View(donation); donation.DonationDate = DateTime.UtcNow; donation.DonorName = donation.IsAnonymous ? "Anonymous Donor" : (User.Identity?.Name ?? "Guest Donor"); donation.TaxCertificateNumber = $"GOTG-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpperInvariant()}"; db.Donations.Add(donation); await db.SaveChangesAsync(); return RedirectToAction(nameof(Certificate), new { id = donation.Id }); } public async Task<IActionResult> Certificate(int id) { var donation = await db.Donations.FindAsync(id); return donation is null ? NotFound() : View(donation); } }
