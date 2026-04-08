using BookSwap.Data.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<Review> ReviewsGiven { get; set; } = new HashSet<Review>();
    public ICollection<Review> ReviewsReceived { get; set; } = new HashSet<Review>();
}