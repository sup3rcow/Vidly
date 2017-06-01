using System;
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
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

        [HttpGet]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres//u konstruktoru od MovieFormViewModel, si stavio da je Id = 0
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);//samo za uvid..

            if (!ModelState.IsValid)//projerava da li propertiji koji su se vratili formom, 
                //propertije od klase koji se nisu vratili ne provjerava 
            {
                var viewModel = new MovieFormViewModel
                {
                    Genres = _context.Genres.ToList(),
                    Name = "????"
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id > 0)//ako je id veci od 0, znas da je edit a ne new movie
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                //movieInDb.DateAdded = movie.DateAdded;//ovo ne jer ne mijenjas ga, a u formi ga mias kao 1.1.0001!!!

                _context.SaveChanges();

                return RedirectToAction("Details", "Movies", new { Id = movieInDb.Id});
            }

            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        public ActionResult EditMovie(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);//ili single or default ko u customerController-u, 
                                                                //sam procijeni koje greske i kako hoces hendlati

            var viewModel = new MovieFormViewModel(movie)//u konstruktoru si definirao mapiranje. COOL
            {
                Genres = _context.Genres.ToList(),//moras ovo slati zbog drop down liste                
            };

            return View("MovieForm", viewModel);
        }
    }
}