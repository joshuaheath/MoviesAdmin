using MoviesAdmin.Models;

namespace MoviesAdmin.ViewModels;

public class MovieDetailsViewModel
{
    public Movie Movie { get; set; } = null!;

    public int PublishedReviewCount { get; set; }
}