namespace BookSwap.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
