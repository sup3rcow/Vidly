using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte Undefined = 0;//statis fieldove definiras na osnovu modela u tablici
        //pa onda koristis njih umjesto magic numbers 0, ili 1..
        public static readonly byte NotAMember = 1;
    }
}
