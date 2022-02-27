using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using MovieViewerProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the Admin Controller. This class provides webpages that allows the admin to view, and assign roles to user, 
  and to edit, delete, and create movies.
  The Userlist method reads all the users and their roles into a list for the admin.
  The AssignRoles methods provides the admin with a form to assign users certain roles, it also adds the role to the user within the database.
  The CreateMovie methods allows the admin to create movies within the database through a form on the webpage.
  The EditMovie methods allows the admin to edit movies that are already in the databse through a form on the webpage.
  The Index method provides the admin with a list of the movies within the database.
  The DeleteMovie methods provide a web page for the admin to delete movies from the database.    
*/
namespace MovieViewerProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly IRoleRepository roleRepo;
        private readonly IMovieRepository movieRepo;

        public AdminController(IUserRepository _userRepo, IRoleRepository _roleRepo, IMovieRepository movieRepository)
        {
            userRepo = _userRepo;
            roleRepo = _roleRepo;
            movieRepo = movieRepository;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await userRepo.ReadAllAsync();
            var userList = users
               .Select(u => new UserListVm
               {
                   Email = u.Email,
                   UserName = u.UserName,
                   RoleNames = string.Join(",", u.Roles.ToArray())
               });
            return View(userList);

        }

        public async Task<IActionResult> AssignRoles()
        {
            var roles = roleRepo.ReadAll();
            var users = await userRepo.ReadAllAsync();

            var model = new AssignRoleVm();
            model.UserNames = users.Select(u => u.UserName).ToList();
            model.RoleNames = roles.Select(r => r.Name).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles(AssignRoleVm roleVM)
        {
            await userRepo.AssignRoleAsync(roleVM.UserName, roleVM.RoleName);
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Index()
        {
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
                    _watched = b._watched

                });
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                userRepo.AddMovie(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        public IActionResult EditMovie(int id)
        {
            var movie = userRepo.MovieDetails(id);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["Movie"] = movie;
            return View(movie);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditMovie([FromForm] Movie movie)
        {
            if (ModelState.IsValid)
            {
                userRepo.EditMovie(movie.Id, movie);
                return RedirectToAction("Index", "Admin");
            }

            return View(movie);
        }


        public IActionResult DeleteMovie(int id)
        {
            var movie = userRepo.MovieDetails(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            userRepo.DeleteMovie(id);
            return RedirectToAction("Index");
        }


    }
}
