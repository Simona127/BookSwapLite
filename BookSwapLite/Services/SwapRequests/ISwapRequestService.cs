namespace BookSwapLite.Services.SwapRequests
{
    using BookSwap.Web.ViewModels.SwapRequests;

    public interface ISwapRequestService
    {
        Task CreateRequestAsync(int bookId, string applicantId);
        Task ApproveAsync(int requestId, string userId);
        Task RejectAsync(int requestId, string userId);
        Task<IEnumerable<SwapRequestOwnerViewModel>> GetRequestsForOwnerAsync(string ownerId);
    }
}
