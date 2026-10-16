using System.ComponentModel.DataAnnotations;
namespace MoviesAdmin.Models
{
    // This class is in charge of Ratings from audience
    // ATTRIBUTES
    // id       (needed for scaffolding and migration)
    // title    (title of movie)
    public class AudienceRating
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50)]
        public string title { get; set; } = string.Empty;
    }
}
