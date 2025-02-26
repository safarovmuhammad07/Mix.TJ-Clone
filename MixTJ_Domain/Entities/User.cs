using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    [MaxLength(1000, ErrorMessage = "Profile cannot be longer than 1000 characters.")]
    public string? Profile { get; set; }
}