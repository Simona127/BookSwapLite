namespace BookSwap.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string GenreName { get; set; } = null!;
        public ICollection<Book> Books { get; set; }
        = new HashSet<Book>();
    }
}
