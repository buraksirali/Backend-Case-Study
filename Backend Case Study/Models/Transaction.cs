using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_Case_Study.Models
{
    public class Transaction
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string OperationType { get; set; }
        public string ShareSymbol { get; set; }
        public string Date { get; set; }
        public int Volume { get; set; }
        public string Name { get; set; }
    }
}
