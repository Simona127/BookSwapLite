namespace BookSwap.Tests
{
    using BookSwap.Core.Services;
    using BookSwap.Core.ViewModels.Books;
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    public class BookServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_Book_When_Genre_Exists()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, GenreName = "Test Genre" });
            await context.SaveChangesAsync();

            var service = new BookService(context);

            var model = new BookFormModel
            {
                Title = "Test Book",
                Author = "Test Author",
                GenreId = 1,
                Description = "Test Description",
                Condition = "New"
            };

            await service.CreateAsync(model, "user1");

            var book = await context.Books.FirstOrDefaultAsync();

            Assert.NotNull(book);
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("user1", book.OwnerId);
        }

        [Fact]
        public async Task CreateAsync_Should_Not_Add_Book_When_Genre_Does_Not_Exist()
        {
            var context = GetDbContext();
            var service = new BookService(context);

            var model = new BookFormModel
            {
                Title = "Invalid Book",
                Author = "Author",
                GenreId = 99, 
                Condition = "New"
            };

            await service.CreateAsync(model, "user1");

            var count = await context.Books.CountAsync();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_False_When_Book_Not_Found()
        {
            var context = GetDbContext();
            var service = new BookService(context);

            var result = await service.DeleteAsync(1, "user1");

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_False_When_User_Is_Not_Owner()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, GenreName = "Test" });

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Test",
                Author = "Author",
                OwnerId = "owner1",
                Condition = "New",
                GenreId = 1
            });

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var result = await service.DeleteAsync(1, "user2");

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Book_When_User_Is_Owner()
        {
            var context = GetDbContext();

            // 🔥 трябва да има genre
            context.Genres.Add(new Genre { Id = 1, GenreName = "Test" });

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Test",
                Author = "Author",
                OwnerId = "user1",
                Condition = "New",
                GenreId = 1
            });

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var result = await service.DeleteAsync(1, "user1");

            Assert.True(result);
            Assert.Equal(0, await context.Books.CountAsync());
        }
    }
}