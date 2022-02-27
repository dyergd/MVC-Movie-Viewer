using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using MovieViewerProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the Movie Controller. This class controls how movie data is returned to the view.
 The Index method displays all the movies that are currently in the database.
 The Details method displays information about individual movies.
 The Watch method displays the information needed to watch an individual movie.
 The Pay method displays the information you need to know when paying for a individual movie.
 The PayMoney method manipulates the database to show that you paid for the movie once
 you complete the Pay page.      
 The CreateRating methods allows the user to create ratings for movies.
*/
namespace MovieViewerProject.Controllers
{
   
    public class MovieController : Controller 
    {
        private readonly IMovieRepository movieRepo;
        private readonly IProfileRepository profileRepo;
        private readonly IUserRepository userRepo;

        public MovieController(IMovieRepository movieRepository, IProfileRepository profileRepository, IUserRepository userRepository)
        {
            movieRepo = movieRepository;
            profileRepo = profileRepository;
            userRepo = userRepository;
        }


        [HttpGet]
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
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            var allMovies = movieRepo.ReadAll();
            var model = allMovies.Select(b =>
                new MovieDetailsVm
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Price = b.Price,
                    LengthInMinutes = b.LengthInMinutes,
                    IMDBUrl = b.IMDBUrl,
                    _watched = b._watched,
                    _rating = b._rating

                });

            var watch = user.Profile.WatchedCollection.Select(w =>
               new Watched
               {
                   MovieId = w.MovieId,
                   Movie = w.Movie,
                   Profile = w.Profile,
                   ProfileId = w.ProfileId,
                   NumOfTimesWatched = w.NumOfTimesWatched,
                   WatchedId = w.WatchedId
               }).ToList(); 

            ViewData["User"] = user;
            ViewData["Watch"] = watch;

            return View(model);
        }


        public IActionResult Details(int id)
        {
            var movie = movieRepo.Details(id);
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

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        public IActionResult Watch(int id)
        {
            var movie = movieRepo.Details(id);
            var user = userRepo.Details(User.Identity.Name);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }           
            else if (!User.Identity.IsAuthenticated)
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
           // else if (user.Profile.HasPaid == false)
            //{
             //   return RedirectToAction("Pay", "Movie", movie.Id);
            //}


            var model =
               new MovieDetailsVm
               {
                   Id = movie.Id,
                   Title = movie.Title,
                   Year = movie.Year,
                   Price = movie.Price,
                   LengthInMinutes = movie.LengthInMinutes,
                   IMDBUrl = movie.IMDBUrl,
                   _watched = movie._watched,
                   _rating = movie._rating
               };

          
            var watchData =
                new Watched
                {
                    WatchedId = 0,
                    MovieId = id,
                    Movie = movie,
                    ProfileId = user.Profile.ProfileId,
                    Profile = user.Profile,
                    NumOfTimesWatched = 1
                };

            if (user.Profile.WatchedCollection.Contains(watchData) == true)
            {          
                user.Profile.WatchedCollection.Remove(watchData);
                watchData.NumOfTimesWatched++;
                user.Profile.WatchedCollection.Add(watchData);
            }
            else
            {
                movieRepo.AddToWatchList(id, watchData, user.Profile.ProfileId);
            }

            ViewData["Movie"] = movie;
            return View(model);
        }

        public IActionResult Pay(int id)
        {
            var movie = movieRepo.Details(id);
            var user = userRepo.Details(User.Identity.Name);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }
            else if (!User.Identity.IsAuthenticated)
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
            else if(user.Profile.HasPaid == true)
            {
                return RedirectToAction("Watch", "Movie", movie.Id);
            }
         
            var model =
              new MovieDetailsVm
              {
                  Id = movie.Id,
                  Title = movie.Title,
                  Year = movie.Year,
                  Price = movie.Price,
                  LengthInMinutes = movie.LengthInMinutes,
                  IMDBUrl = movie.IMDBUrl,
                  _watched = movie._watched,
                  _rating = movie._rating
              };

            

            ViewData["Movie"] = movie;

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult PayMoney(int id)
        {
            var user = userRepo.Details(User.Identity.Name);
            var profile = profileRepo.Details(user.Profile.ProfileId);
            var movie = movieRepo.Details(id);
            

            profileRepo.PayForMovie(movie, profile);

            return RedirectToAction("Watch", "Movie", movie.Id);
        }

        public IActionResult CreateRating(int id)
        {
            var movie = movieRepo.Details(id);
            var user = userRepo.Details(User.Identity.Name);

            ViewData["Movie"] = movie;

            if (movie == null)
            {
                return RedirectToAction("Index");
            }
            else if (!User.Identity.IsAuthenticated)
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

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateMovieRating(Rating rating)
        {
            var movie = movieRepo.Details(rating.MovieId);
            ViewData["Movie"] = movie;

            if (ModelState.IsValid)
            {
                movieRepo.CreateRating(rating.MovieId,rating);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

      

    }
}
