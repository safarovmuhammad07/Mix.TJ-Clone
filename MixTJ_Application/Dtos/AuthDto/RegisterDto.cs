using System.ComponentModel.DataAnnotations;

namespace MixTJ_Application.Dtos.AuthDto;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 6,ErrorMessage = "Username must be between 6 and 50 characters.")]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    public string PasswordConfirm { get; set; }
    public string? Profile { get; set; }
}