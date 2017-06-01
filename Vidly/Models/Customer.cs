using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter customer name --data anotation")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }


        //ovako napravis foreign key u tablici
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Memebership Type")]
        public byte MembershipTypeId { get; set; }//ef konvencijom, skuzi da je ovo foreign key od MembershipType-a

        [Display(Name = "Date of birth")]//ovo nije ok uvijek, jer kad hoces mijenjati labelu, moras rebuildati projekt..
        //mozes pisati i raw html, ali onda sam moras napisati for="Birthdate".. ni to nije idealno, pa kkoristi sta ti odgovara vise
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}
