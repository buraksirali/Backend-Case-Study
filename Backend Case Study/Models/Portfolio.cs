using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_Case_Study.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public PortfolioType Type { get; set; }
    }

    public enum PortfolioType
    {
        Public,
        Private
    }
}
