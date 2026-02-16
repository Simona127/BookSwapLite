namespace BookSwap.Data.Models
{
    using BookSwap.Data.Models.Common;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class SwapRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(Applicant))]
        public string ApplicantId { get; set; } = null!;
        public IdentityUser Applicant { get; set; } = null!;

        [Required]
        public StatusType Status { get; set; }= StatusType.Pending;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
