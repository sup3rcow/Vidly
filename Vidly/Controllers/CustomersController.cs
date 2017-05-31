using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //dummy data
        List<Customer> customers = new List<Customer>()
        {
            new Customer{ Name = "John Smith" , Id=1 },
            new Customer{ Name = "Mary Williams", Id=2 }
        };

        // GET: Customers
        public ActionResult Index()
        {
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = customers.Where(c => c.Id == id).FirstOrDefault();
            /*if (customer == null)//ili object.ReferenceEquals(customer,null), u cshml-u radis provjeru
            {
                return View;
            }*/
            return View(customer);
        }
    }
}