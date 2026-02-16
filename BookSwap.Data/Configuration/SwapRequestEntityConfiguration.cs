namespace BookSwap.Data.Configuration
{
    using BookSwap.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class SwapRequestEntityConfiguration : IEntityTypeConfiguration<SwapRequest>
    {
        public void Configure(EntityTypeBuilder<SwapRequest> builder)
        {
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Status)
                .IsRequired();

            builder.Property(sr=> sr.CreatedOn)
                .IsRequired();

            builder.HasOne(sr => sr.Book)
                .WithMany()
                .HasForeignKey(sr => sr.BookId)
                .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(sr => sr.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}