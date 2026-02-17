namespace BookSwapLite.Services.SwapRequests
{
    public interface ISwapRequestService
    {
        Task CreateRequestAsync(int bookId, string applicantId);
        Task ApproveAsync(int requestId);
        Task RejectAsync(int requestId);
    }
}
