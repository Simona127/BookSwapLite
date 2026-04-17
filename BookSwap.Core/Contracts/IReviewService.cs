using BookSwap.Data.Models;

namespace BookSwap.Core.Contracts
{
    public interface IReviewService
    {
        Task AddReviewAsync(string reviewerId, string reviewedUserId, int rating, string? comment);
        Task<IEnumerable<Review>> GetReviewsForUserAsync(string userId);
        Task<double> GetAverageRatingAsync(string userId);
    }
}
