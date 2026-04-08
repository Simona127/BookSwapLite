using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSwap.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReviewerId { get; set; } = null!;
        public ApplicationUser Reviewer { get; set; } = null!;
        [Required]
        public string ReviewedUserId { get; set; } = null!;
        public ApplicationUser ReviewedUser { get; set; } = null!;
        [Range(1, 5)]
        public int Rating { get; set; }
        [MaxLength(300)]
        public string? Comment { get; set; } = null!;
    }
}