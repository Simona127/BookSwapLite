using BookSwap.Core.Contracts;
using BookSwap.Data;
using BookSwap.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext content;
        public ReviewService(ApplicationDbContext content)
        {
            this.content = content;
        }
        public async Task AddReviewAsync(string reviewerId, string reviewedUserId, int rating, string? comment)
        {
            var review = new Review
            {
                ReviewerId = reviewerId,
                ReviewedUserId = reviewedUserId,
                Rating = rating,
                Comment = comment
            };
            await content.Reviews.AddAsync(review);
            await content.SaveChangesAsync();
        }
        public async Task<IEnumerable<Review>> GetReviewsForUserAsync(string userId)
        {
            return await content.Reviews
                .Where(r => r.ReviewedUserId == userId)
                .Include(r => r.Reviewer)
                .ToListAsync();
        }
        public async Task<double> GetAverageRatingAsync(string userId)
        {
            var reviews = await content.Reviews
                .Where(r => r.ReviewedUserId == userId)
                .ToListAsync();

            if (reviews.Count == 0)
            {
                return 0;
            }
            return reviews.Average(r => r.Rating);
        }
    }
}
