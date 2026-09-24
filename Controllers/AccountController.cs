using GiftOfTheGivers.Web.Models;
using GiftOfTheGivers.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace GiftOfTheGivers.Web.Controllers;
public class AccountController(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn) : Controller {
  [HttpGet] public IActionResult Register() => View(new RegisterViewModel());
  [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Register(RegisterViewModel model) { if (!ModelState.IsValid) return View(model); var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName }; var result = await users.CreateAsync(user, model.Password); if (result.Succeeded) { await users.AddToRoleAsync(user, "Donor"); await signIn.SignInAsync(user, false); return RedirectToAction("Index", "Home"); } foreach (var error in result.Errors) ModelState.AddModelError("", error.Description); return View(model); }
  [HttpGet] public IActionResult Login(string? returnUrl = null) { ViewBag.ReturnUrl = returnUrl; return View(new LoginViewModel()); }
  [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null) { if (!ModelState.IsValid) return View(model); var result = await signIn.PasswordSignInAsync(model.Email, model.Password, false, false); if (result.Succeeded) return LocalRedirect(returnUrl ?? Url.Action("Index", "Home")!); ModelState.AddModelError("", "Invalid email or password."); return View(model); }
  [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Logout() { await signIn.SignOutAsync(); return RedirectToAction("Index", "Home"); }
}
