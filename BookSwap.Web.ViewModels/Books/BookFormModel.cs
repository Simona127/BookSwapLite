using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSwap.Web.ViewModels.Books
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
        public int GenreId { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        = new List<SelectListItem>();

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(30)]
        public string Condition { get; set; } = null!;
    }
}
