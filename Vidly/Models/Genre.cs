using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Genre
    {
        public short Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
