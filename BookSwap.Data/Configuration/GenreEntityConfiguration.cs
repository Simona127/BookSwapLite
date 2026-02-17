namespace BookSwap.Data.Configuration
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.GenreName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(g => g.Books)
                .WithOne(b => b.Genre)
                .HasForeignKey(b => b.GenreId);

            builder.HasData(
                    new Genre { Id = 1, GenreName = "Drama" },
                    new Genre { Id = 2, GenreName = "Thriller" },
                    new Genre { Id = 3, GenreName = "Science Fiction" },
                    new Genre { Id = 4, GenreName = "Fantasy" },
                    new Genre { Id = 5, GenreName = "Mystery" },
                    new Genre { Id = 6, GenreName = "Romance" }
                );
        }
    }
}
