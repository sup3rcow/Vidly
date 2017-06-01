using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Undefined || 
                customer.MembershipTypeId == MembershipType.NotAMember)
            //0(Undefined) je ako korisnik nista ne odabere, jer je 0 default numeric value, 
            //bithdate provjeravas samo ako je korisnik odabrao membertype veci od 1(NotAMember)..
            //ovo ti automtski dopusti da spremis korisnika da mu ne definiras brith date ali membertype mora biti 1(NotAMember)
            {
                return ValidationResult.Success;//success je static field
            }
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birth date is required");//moras napraviti instancu
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;//value pises jer je nullable datetime

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Not 18 years old customer"); 
        }
    }
}
