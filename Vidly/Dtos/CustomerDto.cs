using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        //public MembershipType MembershipType { get; set; }// u dto ne ubacujes klase iz modela,nego moras napraviti novi dto od te klase
        public MembershipTypeDto MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}