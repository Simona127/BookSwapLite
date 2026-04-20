using System.ComponentModel.DataAnnotations;

namespace BookSwap.Core.ViewModels.Books
{
    public class BookFormModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Author { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a genre")]
        public int GenreId { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required]
        [StringLength(30)]
        public string Condition { get; set; } = null!;
    }
}
