namespace BookSwap.Services.Books
{
    using BookSwap.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> GetAllBooksAsync();

        Task<IEnumerable<SelectListItem>> GetGenresAsync();

        Task CreateAsync(BookFormModel model, string userId);

        Task<BookDetailsViewModel> GetDetailsAsync(int id);

        Task<BookFormModel> GetForEditAsync(int id); 

        Task UpdateAsync(int id, BookFormModel model);

        Task DeleteAsync(int id);
    }
}
