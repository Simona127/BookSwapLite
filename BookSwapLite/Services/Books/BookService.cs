namespace BookSwap.Services.Books
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using BookSwap.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc.Rendering;
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
                    Author = b.Author
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GenreName
                })
                .ToListAsync();
        }
        public async Task CreateAsync(BookFormModel model, string userId)
        {
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
        public async Task<BookDetailsViewModel> GetDetailsAsync(int id)
        {
            var book = await context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Book not found.");
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
        public async Task<BookFormModel> GetForEditAsync(int id)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                throw new ArgumentException("Book not found.");
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
        public async Task UpdateAsync(int id, BookFormModel model)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }

            book.Title = model.Title;
            book.Author = model.Author;
            book.GenreId = model.GenreId;
            book.Description = model.Description;
            book.Condition = model.Condition;

            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}
