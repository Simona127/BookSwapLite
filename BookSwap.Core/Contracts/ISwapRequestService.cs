namespace BookSwap.Core.Contracts
{
    using BookSwap.Core.ViewModels.SwapRequests;

    public interface ISwapRequestService
    {
        Task CreateRequestAsync(int bookId, string applicantId);
        Task ApproveAsync(int requestId, string userId);
        Task RejectAsync(int requestId, string userId);
        Task<IEnumerable<SwapRequestOwnerViewModel>> GetRequestsForOwnerAsync(string ownerId);
    }
}
