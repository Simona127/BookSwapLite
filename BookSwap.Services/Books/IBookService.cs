namespace BookSwap.Services.Books
{
    using BookSwap.Web.ViewModels.Books;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task AddAsync(BookModel model, string userId);
    }
}
