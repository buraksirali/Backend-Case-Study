using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Case_Study.Models
{
    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public int? PortfolioId { get; set; }
    }
}
