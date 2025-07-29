using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Additional properties can be added here if needed
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

}