using System.ComponentModel.DataAnnotations;
namespace GiftOfTheGivers.Web.ViewModels;
public class RegisterViewModel { [Required] public string FirstName { get; set; } = ""; [Required] public string LastName { get; set; } = ""; [Required, EmailAddress] public string Email { get; set; } = ""; [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)] public string Password { get; set; } = ""; }
public class LoginViewModel { [Required, EmailAddress] public string Email { get; set; } = ""; [Required, DataType(DataType.Password)] public string Password { get; set; } = ""; }
