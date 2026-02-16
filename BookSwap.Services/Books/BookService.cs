namespace BookSwap.Services.Books
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using BookSwap.Web.ViewModels.Books;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;
        public BookService(ApplicationDbContext context) 
        { 
            this.context = context;
        }
        public async Task AddAsync(BookModel model, string userId)
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
