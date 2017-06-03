using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MembershipTypeDto
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]//mosh tu nije pisao data anotation, mozda je ovaj dto, koristi samo za get, a ne i za push?
        public string Name { get; set; }
    }
}
