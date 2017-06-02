using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Dtos;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance;

            //npr ako pozovemo POST kroz web api, ulazni objekt je CustomerDto, i njega ne mozemo kastati direktno u Customer,
            //nego ga moramo transformirati sa automapperom..
            //ovo je dosta "prljavo", a i ako ova validacija ne prodje, web api ce vratiti 400 bad request
            //mosh je ovaj problem resio tako sto je ibacio iz CustomerDto.cs [Min18YearsIfAMember]

            //mosh kaze da imjenis formu, da iz nje postas kroz web api, a tu u ovoj validaciji da kastas objekt u CustomerDto..
            //uradi to za vjezbu jednom!!
            Customer customer = new Customer();
            try
            {
                //ovo je ako npr kreiramo novog customera kroz formu
                customer = (Customer)obj;
            }
            catch (Exception)
            {
                try
                {
                    //ovo je kad kreiramo novog usera kroz web api
                    customer = Mapper.Map<CustomerDto, Customer>((CustomerDto)obj);
                }
                catch (Exception)
                {

                    //throw;
                }
                //throw;
            }


            //var customer = (Customer)validationContext.ObjectInstance;
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
