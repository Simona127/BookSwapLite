namespace BookSwap.Web.ViewModels.Books
{
    using System.ComponentModel.DataAnnotations;
    public class BookFormModel
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Author { get; set; } = null!;

        [Required]
        public int GenreId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(30)]
        public string Condition { get; set; } = null!;
    }
}
