using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        //Navigation property
        [ValidateNever]
        public List<Movie> Movies { get; set; } = new List<Movie>();

        [Required]
        [Display(Name = "Rating")]
        [StringLength(50)]
        public string title { get; set; } = string.Empty;
    }
}
