namespace BookSwap.Core.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }=null!;
        public string Author { get; set; }=null!;
        public string Genre { get; set; }=null!;
        public string? Description { get; set; }
        public string Condition { get; set; } = null!;
    }
}
