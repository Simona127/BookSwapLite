namespace BookSwap.Tests
{
    using BookSwap.Core.Services;
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using BookSwap.Data.Models.Common;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    public class SwapRequestServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateRequestAsync_Should_Add_Request()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                Author = "Author",
                OwnerId = "owner1",
                Condition = "New",
                GenreId = 1
            });

            await context.SaveChangesAsync();

            var service = new SwapRequestService(context);

            await service.CreateRequestAsync(1, "user2");

            var request = await context.SwapRequests.FirstOrDefaultAsync();

            Assert.NotNull(request);
            Assert.Equal("user2", request.ApplicantId);
            Assert.Equal(1, request.BookId);
        }

        [Fact]
        public async Task CreateRequestAsync_Should_Throw_When_Requesting_Own_Book()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                Author = "Author",
                OwnerId = "user1",
                Condition = "New",
                GenreId = 1
            });

            await context.SaveChangesAsync();

            var service = new SwapRequestService(context);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateRequestAsync(1, "user1"));
        }

        [Fact]
        public async Task ApproveAsync_Should_Approve_Request()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                Author = "Author",
                OwnerId = "owner1",
                Condition = "New",
                GenreId = 1
            };

            context.Books.Add(book);

            context.SwapRequests.Add(new SwapRequest
            {
                Id = 1,
                BookId = 1,
                ApplicantId = "user2",
                Status = StatusType.Pending
            });

            await context.SaveChangesAsync();

            var service = new SwapRequestService(context);

            await service.ApproveAsync(1, "owner1");

            var request = await context.SwapRequests.FirstAsync();

            Assert.Equal(StatusType.Approved, request.Status);
        }

        [Fact]
        public async Task RejectAsync_Should_Reject_Request()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                Author = "Author",
                OwnerId = "owner1",
                Condition = "New",
                GenreId = 1
            };

            context.Books.Add(book);

            context.SwapRequests.Add(new SwapRequest
            {
                Id = 1,
                BookId = 1,
                ApplicantId = "user2",
                Status = StatusType.Pending
            });

            await context.SaveChangesAsync();

            var service = new SwapRequestService(context);

            await service.RejectAsync(1, "owner1");

            var request = await context.SwapRequests.FirstAsync();

            Assert.Equal(StatusType.Rejected, request.Status);
        }

        [Fact]
        public async Task ApproveAsync_Should_Throw_When_User_Not_Owner()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                Author = "Author",
                OwnerId = "owner1",
                Condition = "New",
                GenreId = 1
            });

            context.SwapRequests.Add(new SwapRequest
            {
                Id = 1,
                BookId = 1,
                ApplicantId = "user2",
                Status = StatusType.Pending
            });

            await context.SaveChangesAsync();

            var service = new SwapRequestService(context);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                service.ApproveAsync(1, "user3"));
        }
    }
}