namespace BookSwapLite.Services.SwapRequests
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using BookSwap.Data.Models.Common;
    using Microsoft.EntityFrameworkCore;

    public class SwapRequestService : ISwapRequestService
    {
        private readonly ApplicationDbContext context;
        public SwapRequestService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateRequestAsync(int bookId, string applicantId)
        {
            bool requestExists = await context.SwapRequests
                .AnyAsync(sr => sr.BookId == bookId && sr.ApplicantId == applicantId);

            if (requestExists)
            {
                throw new InvalidOperationException("You already requested this book.");
            }

            var swapRequest = new SwapRequest
            {
                BookId = bookId,
                ApplicantId = applicantId
            };

            await context.SwapRequests.AddAsync(swapRequest);
            await context.SaveChangesAsync();
        }

        public async Task ApproveAsync(int requestId)
        {
            var swapRequest = await context.SwapRequests
                .FirstOrDefaultAsync(sr => sr.Id == requestId);

            if (swapRequest == null)
            {
                throw new InvalidOperationException("Request not found.");
            }

                swapRequest.Status = StatusType.Approved;
                await context.SaveChangesAsync();
        }

        public async Task RejectAsync(int requestId)
        {
            var swapRequest = await context.SwapRequests
                .FirstOrDefaultAsync(sr => sr.Id == requestId);

            if (swapRequest == null)
            {
                throw new InvalidOperationException("Request not found.");
            }

                swapRequest.Status = StatusType.Rejected;
                await context.SaveChangesAsync();
        }
    }
}
