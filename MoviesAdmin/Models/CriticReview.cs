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

        [Required]
        [Display(Name = "Description")]
        [StringLength(500)]
        public string description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Star Rating")]
        [Range(1, 5)]
        public int rating { get; set; }

        [Required]
        [Display(Name = "Is Published?")]
        public bool isPublished { get; set; } = false;

        [Required]
        [Display(Name = "Created By")]
        [StringLength(40)]
        public string createdBy { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime createdDate { get; set; } = DateTime.Now;

    }
}
