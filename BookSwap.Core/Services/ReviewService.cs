using BookSwap.Core.Contracts;
using BookSwap.Data;
using BookSwap.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext context;

        public ReviewService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddReviewAsync(string reviewerId, string reviewedUserId, int rating, string? comment)
        {
            if (string.IsNullOrEmpty(reviewerId) || string.IsNullOrEmpty(reviewedUserId))
            {
                throw new ArgumentException("Invalid user data.");
            }

            var review = new Review
            {
                ReviewerId = reviewerId,
                ReviewedUserId = reviewedUserId,
                Rating = rating,
                Comment = comment
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsForUserAsync(string userId)
        {
            return await context.Reviews
                .Where(r => r.ReviewedUserId == userId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(string userId)
        {
            var reviews = await context.Reviews
                .Where(r => r.ReviewedUserId == userId)
                .ToListAsync();

            if (!reviews.Any())
            {
                return 0;
            }

            return reviews.Average(r => r.Rating);
        }
    }
}