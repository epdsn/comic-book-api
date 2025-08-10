using Microsoft.AspNetCore.Identity;

namespace ComicBookApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here if needed
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        // Subscription and payment properties
        // Not being used currently, placeholder for now
        public string? StripeCustomerId { get; set; }
        public string? StripeSubscriptionId { get; set; }
        public DateTime? SubscriptionExpiresAt { get; set; }
        public bool IsPremium { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
    }
}