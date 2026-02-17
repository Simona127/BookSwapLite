namespace BookSwap.Web.ViewModels.SwapRequests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SwapRequestOwnerViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }= null!;
        public string ApplicantUsername { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; } = null!;
    }
}
