namespace BookSwap.Services.Books
{
    using BookSwap.Web.ViewModels.Books;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task CreateAsync(BookFormModel model, string userId);

        Task<IEnumerable<BookIndexViewModel>> GetAllAsync();
    }
}
