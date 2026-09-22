namespace MoviesAdmin.Models
{
    // This class is in charge of data relating movies:
    // ATTRIBUTES:
    // title
    // synopsis
    // genre
    // rating (eg PG-13)
    // runtimeMin (how long the movie will be)
    // release data (done as a date time object)
    public class MovieData
    {
        public string title { get; set; } = string.Empty;
        public string synopsis { get; set; } = string.Empty;
        
        public string genre { get; set; } = string.Empty;
        public string rating  { get; set; }

        public int runtimeMin { get; set; }

        public DateTime releaseDate { get; set; } = DateTime.Now;
    }
}
