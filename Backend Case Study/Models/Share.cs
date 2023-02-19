using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Case_Study.Models
{
    public class Share
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string LastUpdate { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Symbol { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(10, 99)]
        public int Rate { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required]
        public int PortfolioId { get; set; }
    }
}
