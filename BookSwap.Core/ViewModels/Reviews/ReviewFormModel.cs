namespace BookSwap.Core.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;
    public class ReviewFormModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }
    }
}