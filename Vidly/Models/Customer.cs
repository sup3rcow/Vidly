using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }


        //ovako napravis foreign key u tablici
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }//ef konvencijom, skuzi da je ovo foreign key od MembershipType-a

        public DateTime? Birthdate { get; set; }
    }
}
