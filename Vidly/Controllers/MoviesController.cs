using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek."};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);

            //ViewData["Movie"] = movie;
            //return View();    
            //mozes i ovako da posaljes podatke u view, ali onda u cshtml-u umjesto @Model.Name
            //koristis @(((Movie)ViewData["Movie"]).Name), nemoj nikada koristiti ovaj nacin.. ovo je staro

            //ViewBag.Movie = movie;//mozes i ovako poslati podatke u cshtml, nemoj ni ovo nikada oristiti

            //return View(movie);
        }

        [Route("movies/released/{year}/{month:regex(\\d{1,2}):range(1,12)}")]//tu moras koristiti \\d a ne @"\d"
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content($"{year}/{month}");
        }

        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

        //npr http://localhost:53965/Movies/index2?sortby=333asa&pageindex=4
        public ActionResult Index2(int? pageIndex = 1, string sortby = "Name" )
        {
            return Content($"age index: {pageIndex}, sort by: {sortby}");
        }


        public ActionResult Index()
        {
            return View(_context.Movies.Include(m => m.Genre).ToList());
        }

        public ActionResult Details(int id)
        {
            return View(_context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Genre)
                .SingleOrDefault());
        }


    }
}