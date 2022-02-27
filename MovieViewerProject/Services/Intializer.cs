using Microsoft.AspNetCore.Identity;
using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the Intializer class. 
  The SeedUsersAsync method creates the roles we use in the application, and automatically adds the admin role to the admin user.
  The Seed Movies method creates 10 movies in the database.
*/
namespace MovieViewerProject.Services
{
    public class Intializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository userRepo;

        public Intializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUserRepository _userRepo)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            userRepo = _userRepo;
        }


        public async Task SeedUsersAsync()
        {
            _db.Database.EnsureCreated();

            if (!_db.Roles.Any(r => r.Name == "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if (!_db.Roles.Any(r => r.Name == "Movie Connessuir"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Movie Connessuir" });
            }

            if (!_db.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@test.com",
                    UserName = "admin@test.com",
                };
                await _userManager.CreateAsync(user, "Pass123!");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

        }

        public void SeedMovies()
        {
            _db.Database.EnsureCreated();

            if (_db.Movies.Any(m => m.Id != 10))
            {
                var movie = new Movie
                {
                    Id = 1,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt0848228/",
                    Title = "Avengers",
                    LengthInMinutes = 143,
                    Year = 2012
                };

                var movie2 = new Movie
                {
                    Id = 2,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt2395427/",
                    Title = "Avengers Age Of Ultron",
                    LengthInMinutes = 141,
                    Year = 2015
                };

                var movie3 = new Movie
                {
                    Id = 3,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt4154756/",
                    Title = "Avengers Infinity War",
                    LengthInMinutes = 149,
                    Year = 2018
                };

                var movie4 = new Movie
                {
                    Id = 4,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt4154796/",
                    Title = "Avengers Endgame",
                    LengthInMinutes = 181,
                    Year = 2019
                };

                var movie5 = new Movie
                {
                    Id = 5,
                    Price = 5,
                    IMDBUrl = "https://www.imdb.com/title/tt0145487/",
                    Title = "Spiderman",
                    LengthInMinutes = 121,
                    Year = 2002
                };

                var movie6 = new Movie
                {
                    Id = 6,
                    Price = 5,
                    IMDBUrl = "https://www.imdb.com/title/tt0316654/",
                    Title = "Spiderman 2",
                    LengthInMinutes = 127,
                    Year = 2004
                };

                var movie7 = new Movie
                {
                    Id = 7,
                    Price = 5,
                    IMDBUrl = "https://www.imdb.com/title/tt0413300/",
                    Title = "Spiderman 3",
                    LengthInMinutes = 139,
                    Year = 2007
                };

                var movie8 = new Movie
                {
                    Id = 8,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt1431045/",
                    Title = "Deadpool",
                    LengthInMinutes = 108,
                    Year = 2016
                };

                var movie9 = new Movie
                {
                    Id = 9,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt5463162/",
                    Title = "Deadpool 2",
                    LengthInMinutes = 119,
                    Year = 2018
                };

                var movie10 = new Movie
                {
                    Id = 10,
                    Price = 12,
                    IMDBUrl = "https://www.imdb.com/title/tt2911666/",
                    Title = "John Wick",
                    LengthInMinutes = 101,
                    Year = 2014
                };

                userRepo.AddMovie(movie);
                userRepo.AddMovie(movie2);
                userRepo.AddMovie(movie3);
                userRepo.AddMovie(movie4);
                userRepo.AddMovie(movie5);
                userRepo.AddMovie(movie6);
                userRepo.AddMovie(movie7);
                userRepo.AddMovie(movie8);
                userRepo.AddMovie(movie9);
                userRepo.AddMovie(movie10);

            }
        }
    }

}
