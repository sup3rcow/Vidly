using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class GenreDto
    {
        public short Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
