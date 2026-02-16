namespace BookSwap.Data.Models
{
    using BookSwap.Data.Models.Common;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class SwapRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(Applicant))]
        public string ApplicantId { get; set; } = null!;
        public virtual IdentityUser Applicant { get; set; } = null!;

        [Required]
        public StatusType Status { get; set; }= StatusType.Pending;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
