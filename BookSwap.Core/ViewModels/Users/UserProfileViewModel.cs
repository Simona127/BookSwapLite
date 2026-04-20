namespace BookSwap.Core.ViewModels.Users
{
    using BookSwap.Core.ViewModels.Books;

    public class UserProfileViewModel
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public double Rating { get; set; }

        public IEnumerable<BookIndexViewModel> Books { get; set; }
            = new List<BookIndexViewModel>();
    }
}