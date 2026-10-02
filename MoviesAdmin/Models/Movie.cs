using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    // This class is in charge of data relating movies:
    // ATTRIBUTES:
    // id
    // title
    // synopsis
    // genre
    // rating (eg PG-13)
    // runtimeMin (how long the movie will be)
    // release data (done as a date time object)
    public class Movie
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50)]
        public string title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Runtime (Min)")]
        [Range(1, 240)] //4 Hour max
        public int runtimeMin { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength (700)]
        public string synopsis { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Genre")]
        [StringLength (50)]
        public string genre { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Rating")]
        [StringLength(6)]
        public string rating { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime releaseDate { get; set; } = DateTime.Now;
    }
}
