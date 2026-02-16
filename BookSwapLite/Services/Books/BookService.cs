namespace BookSwap.Services.Books
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using BookSwap.Web.ViewModels.Books;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;
        public BookService(ApplicationDbContext context) 
        { 
            this.context = context;
        }
        public async Task<IEnumerable<BookIndexViewModel>> GetAllAsync()
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
    }
}
