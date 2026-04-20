namespace BookSwap.Tests
{
    using BookSwap.Core.Services;
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    public class ReviewServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddReviewAsync_Should_Add_Review_Successfully()
        {
            var context = GetDbContext();
            var service = new ReviewService(context);

            string reviewerId = "reviewer1";
            string reviewedUserId = "user1";

            await service.AddReviewAsync(reviewerId, reviewedUserId, 5, "Great user!");

            var review = await context.Reviews.FirstOrDefaultAsync();

            Assert.NotNull(review);
            Assert.Equal(reviewerId, review.ReviewerId);
            Assert.Equal(reviewedUserId, review.ReviewedUserId);
            Assert.Equal(5, review.Rating);
            Assert.Equal("Great user!", review.Comment);
        }

        [Fact]
        public async Task GetAverageRatingAsync_Should_Return_Correct_Average()
        {
            var context = GetDbContext();

            context.Reviews.AddRange(
                new Review { ReviewerId = "r1", ReviewedUserId = "user1", Rating = 5 },
                new Review { ReviewerId = "r2", ReviewedUserId = "user1", Rating = 3 }
            );

            await context.SaveChangesAsync();

            var service = new ReviewService(context);

            var result = await service.GetAverageRatingAsync("user1");

            Assert.Equal(4, result);
        }

        [Fact]
        public async Task GetReviewsForUserAsync_Should_Return_Only_Requested_User_Reviews()
        {
            var context = GetDbContext();

            context.Reviews.AddRange(
                new Review { ReviewerId = "r1", ReviewedUserId = "user1", Rating = 5 },
                new Review { ReviewerId = "r2", ReviewedUserId = "user1", Rating = 4 },
                new Review { ReviewerId = "r3", ReviewedUserId = "user2", Rating = 3 }
            );

            await context.SaveChangesAsync();

            var service = new ReviewService(context);

            var result = await service.GetReviewsForUserAsync("user1");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddReviewAsync_Should_Save_Multiple_Reviews()
        {
            var context = GetDbContext();
            var service = new ReviewService(context);

            await service.AddReviewAsync("r1", "user1", 5, "Excellent");
            await service.AddReviewAsync("r2", "user1", 4, "Good");

            var count = await context.Reviews.CountAsync();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetAverageRatingAsync_Should_Return_Zero_When_No_Reviews()
        {
            var context = GetDbContext();
            var service = new ReviewService(context);

            var result = await service.GetAverageRatingAsync("user1");

            Assert.Equal(0, result);
        }
    }
}