using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSwap.Data.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
