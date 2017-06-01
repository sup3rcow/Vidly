using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
        public IEnumerable<Customer> GetCustomers()
        {

            return _context.Customers.ToList();
        }

        // GET api/customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);//ovo je konvencija, da se ovo vraca ako se ne nadje trazeno
            }
            return customer;
        }

        // POST api/customers
        [HttpPost] //ako naziv actiona pocinje sa Post, ne moras pisati [HttpPost], ai mosh kaze da ga uvek pisemo, jer ako promenis naziv, kod ti nece raditi
        public Customer PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);//ovo je konvencija
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // PUT api/customer/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)//moze vratiti void ili customer
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);//ovo je konvencija
            }

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            CustomerInDb.Name = customer.Name;
            CustomerInDb.Birthdate = customer.Birthdate;
            CustomerInDb.MembershipTypeId = customer.MembershipTypeId;
            CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            _context.SaveChanges();
        }

        // DELETE api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(CustomerInDb);
            _context.SaveChanges();
        }
    }
}
