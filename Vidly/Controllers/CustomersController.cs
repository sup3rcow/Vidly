using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)//mvc automatski pomapira NewCustomerModel na Customer model
        {
            if (!ModelState.IsValid)//provjeris da li je dobro pomapirano.. ako nije vratis na doradu, ovo doradi..
                                    //mada mozda bolje ne ovo raditi, nego ubaciti validacije na polja
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var viewModel = new CustomerFormViewModel
                {
                    MembershipTypes = _context.MembershipTypes.ToList(),//moras poslati opet podatke za dropdown
                    Customer = customer
                };

                return View("CustomerForm", viewModel);
            }


            else if (customer.Id != 0)//ako je 0, znaci da trebas insert, inace je update
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);//single, odmah izvrsava query
                                                                                       //ne pozivas singleOrDefault, jer ne zelis to hendlati, neka izbaci exceprion..

                //TryUpdateModel(customerInDb);//nemoj ovako, jer otvara rupe u sigurnosti

                //ovako.. rucno, ili pomocu automapera, koji sam usporedi 2 objekta i napravi ovo ispod.. Mapper.Map(customer, customerInDb)
                //ako koristis AutoMapper, oda se preporuca da se za ulazni parametar od konteolera koristi Dto..ž
                //gde su definirani samo atributi koji se trebaju osvjeziti..
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

                _context.SaveChanges();

                return RedirectToAction("Details", new { Id = customer.Id });
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Edit(int Id)
        {
            var viewModel = new CustomerFormViewModel
            {
                Customer = _context.Customers.SingleOrDefault(c => c.Id == Id),
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            if (viewModel.Customer == null)
            {
                return HttpNotFound();
            }

            return View("CustomerForm", viewModel);
        }
    }
}