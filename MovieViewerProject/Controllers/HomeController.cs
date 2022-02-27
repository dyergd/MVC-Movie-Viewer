using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieViewerProject.Models;
using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using MovieViewerProject.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
/*This is the home Controller. This class controls how various data is returned to the view.
  For example this class provides methods to generate the views needed for our about and privacy pages.
  The Index method displays all the movies that a you have watched.
  The About method provides the action to show you the about page.
  The error method provies an error model in the event of a error.
*/
namespace MovieViewerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository movieRepo;
        private readonly IProfileRepository profileRepo;
        private readonly IUserRepository userRepo;
       

        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, 
            IProfileRepository profileRepository, IUserRepository userRepository)
        {
            _logger = logger;
            movieRepo = movieRepository;
            profileRepo = profileRepository;
            userRepo = userRepository;
        }

     

        public IActionResult Index()
        {
            var user = userRepo.Details(User.Identity.Name);
            

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/account/login");  
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }


            var profile = user.Profile;

            if (profile.WatchedCollection.Contains(null))
            {
                return RedirectToAction("Index", "Movie");
            }

             var model = profile.WatchedCollection.Select(w => 
                new MovieDetailsVm
                {
                        Id = w.MovieId,
                        Title = w.Movie.Title,
                        Year = w.Movie.Year,
                        Price = w.Movie.Price,
                        LengthInMinutes = w.Movie.LengthInMinutes,
                        IMDBUrl = w.Movie.IMDBUrl,
                        _watched = w.Movie._watched,
                        _rating = w.Movie._rating
                });

            var watch = profile.WatchedCollection.Select(w =>
                new Watched
                {
                    MovieId = w.MovieId,
                    Movie = w.Movie,
                    Profile = w.Profile,
                    ProfileId = w.ProfileId,
                    NumOfTimesWatched = w.NumOfTimesWatched,
                    WatchedId = w.WatchedId
                });


            ViewData["Watch"] = watch;
            
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
