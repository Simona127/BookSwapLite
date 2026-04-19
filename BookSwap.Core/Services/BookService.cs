namespace BookSwap.Core.Services
{
    using BookSwap.Core.Contracts;
    using BookSwap.Core.ViewModels.Books;
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;

        public BookService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksAsync()
        {
            return await context.Books
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    OwnerId = b.OwnerId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            return await context.Genres
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.GenreName
                })
                .ToListAsync();
        }

        public async Task CreateAsync(BookFormModel model, string userId)
        {
            bool genreExists = await context.Genres
                .AnyAsync(g => g.Id == model.GenreId);

            if (!genreExists)
            {
                return;
            }

            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                GenreId = model.GenreId,
                Description = model.Description,
                Condition = model.Condition,
                OwnerId = userId
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task<BookDetailsViewModel?> GetDetailsAsync(int id)
        {
            var book = await context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            return new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre.GenreName,
                Description = book.Description,
                Condition = book.Condition
            };
        }

        private async Task<bool> IsAdminAsync(string userId)
        {
            return await context.UserRoles
                .Join(context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.UserId, r.Name })
                .AnyAsync(x => x.UserId == userId && x.Name == "Administrator");
        }

        public async Task<BookFormModel?> GetForEditAsync(int id, string userId)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            var isAdmin = await IsAdminAsync(userId);

            if (book.OwnerId != userId && !isAdmin)
            {
                return null;
            }

            return new BookFormModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                GenreId = book.GenreId,
                Description = book.Description,
                Condition = book.Condition
            };
        }

        public async Task<bool> UpdateAsync(int id, BookFormModel model, string userId)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return false;
            }

            var isAdmin = await IsAdminAsync(userId);

            if (book.OwnerId != userId && !isAdmin)
            {
                return false;
            }

            bool genreExists = await context.Genres
                .AnyAsync(g => g.Id == model.GenreId);

            if (!genreExists)
            {
                return false;
            }

            book.Title = model.Title;
            book.Author = model.Author;
            book.GenreId = model.GenreId;
            book.Description = model.Description;
            book.Condition = model.Condition;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return false;
            }

            var isAdmin = await IsAdminAsync(userId);

            if (book.OwnerId != userId && !isAdmin)
            {
                return false;
            }

            bool hasRequests = await context.SwapRequests
                .AnyAsync(sr => sr.BookId == id);

            if (hasRequests)
            {
                return false;
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return true;
        }
    }
}