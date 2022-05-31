using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming2Server.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int RatingId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Review { get; set; }

        public DateTime? Created { get; set; }
    }
}
