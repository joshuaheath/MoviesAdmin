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
        [Range(15, 240)] //4 Hour max
        public int runtimeMin { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength (700)]
        public string synopsis { get; set; } = string.Empty;

        [Display(Name = "Image Url")]
        public string imageUrl {  get; set; } = string.Empty;

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime releaseDate { get; set; } = DateTime.Now;
    }
}
