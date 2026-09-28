using Microsoft.EntityFrameworkCore;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
    public DbSet<MoviesAdmin.Models.Movie> Movie { get; set; } = default!;
}
