using Microsoft.EntityFrameworkCore;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
    public DbSet<MoviesAdmin.Models.Movie> Movie { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.CriticReview> CriticReview { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.AudienceRating> AudienceRating { get; set; } = default!;
}
