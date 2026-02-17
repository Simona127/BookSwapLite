namespace BookSwap.Web.ViewModels.SwapRequests
{
    using BookSwap.Data.Models.Common;
    using System;

    public class SwapRequestOwnerViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }= null!;
        public string ApplicantUsername { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public StatusType Status { get; set; }
    }
}
