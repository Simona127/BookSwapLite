namespace BookSwapLite.Services.SwapRequests
{
    using BookSwap.Web.ViewModels.SwapRequests;

    public interface ISwapRequestService
    {
        Task CreateRequestAsync(int bookId, string applicantId);
        Task ApproveAsync(int requestId);
        Task RejectAsync(int requestId);
        Task<IEnumerable<SwapRequestOwnerViewModel>> GetRequestsForOwnerAsync(string ownerId);
    }
}
