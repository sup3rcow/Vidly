using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/customers
        public IHttpActionResult GetCustomers()
        {

            var customerDtos = _context.Customers.ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);//nema (), jer delegiras mapira ne pozivas ga

            return Ok(customerDtos);
        }

        // GET api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);//ovo je konvencija, da se ovo vraca ako se ne nadje trazeno
                return NotFound();
            }
            //return Mapper.Map<Customer,CustomerDto>(customer);//tu imas (), jer pozivas mapiranje odmah
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));//ovako je koristis IHttpActionResult
        }

        // POST api/customers
        [HttpPost] //ako naziv actiona pocinje sa Post, ne moras pisati [HttpPost], ai mosh kaze da ga uvek pisemo, jer ako promenis naziv, kod ti nece raditi
        public IHttpActionResult PostCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);//ovo je konvencija
                return BadRequest();//pises ovako jer vracas IHttpActionResult!!!!
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;//kad spremis, baza ce kreirati Id, pa ga vratis korisniku, jer on salje CustomerDto bez Id propertija

            //return customerDto; //vrati Ok 200, ne vise ovako, jer koristis IHttpActionResult

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);//vrati 201 Created, sto je Konvencija
            //u hederu od responsa ces imati key: location = http://localhost:23423/api/customer/ {id od novokreirano objekta}
            //!!!!!!!!!!!!!!!

        }

        // PUT api/customer/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)//moze vratiti void ili customer
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);//ovo je konvencija
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            /*umjesto ovoga pises, jednu liniju kod-a ispod :)
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;*/

            //Mapper.Map<CustomerDto, Customer>(customerDto, CustomerInDb);
            Mapper.Map(customerDto, customerInDb);//krace pises ovako, jer kompajler sam skuzi kojeg su tipa

            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/customer/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(CustomerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
