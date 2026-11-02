using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace MoviesAdmin.Models
{
    // This class is in charge of reviews made by critics
    // ATTRIBUTES
    // id           (needed for scaffolding and migration)
    // description  (the main text body of the review - TBD)
    // rating       (1-5 stars)
    // isPublished  (boolean if viewable to the public)
    // createdBy    (what critic created the review)
    // createdDate  (the date the review was created)
    public class CriticReview
    {
        public int id { get; set; }

        //FK
        [Display(Name ="Movie")]
        public int movieId { get; set; }

        // nav prop
        [ValidateNever]
        public Movie movie { get; set; } = null!;


        [Required]
        [Display(Name = "Description")]
        [StringLength(1000)]
        public string description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Rating (1 - 5 starts)")]
        [Range(1, 5)]
        public int rating { get; set; }

        [Required]
        [Display(Name = "Published?")]
        public bool isPublished { get; set; } = false;

        [Required]
        [Display(Name = "Created By")]
        [StringLength(40)]
        public string createdBy { get; set; } = string.Empty;

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime createdDate { get; set; } = DateTime.Now;

    }
}
