using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //_context treba disposati? do sada niko ovo nije radio u primjerima
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();    
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(_context.Customers.Include(c => c.MembershipType).ToList());
            //ili Include("MembershipType"), i onda moras using System.Data.Entity;, ali tada ovisis os magicnom stringu, i akko ga lose
            //napises, gresku ces videti tek u runtime-u
            //ako ne pozoves tu to list, query ce se izvrsiti u cshtml-u u foreach-u
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);//SigleOrDefault odmah izvrsava query
            /*if (customer == null)//ili object.ReferenceEquals(customer,null), u cshml-u radis provjeru
            {
                return View;
            }*/
            return View(customer);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
    }
}