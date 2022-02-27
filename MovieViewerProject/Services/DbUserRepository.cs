using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the DbUserRepository. 
 * This class fills out the method stubs from the IUserRepository, and is used mainly by the admin controller.
 * The ReadAsync method reads a user and it's roles from the database.
 * The Details method returns the user profile of a individual user.
 * The ReadAllAsync method reads all the users and their roles from the database.
 * The AssignRoleAsync method assigns a specified role to a specified user.
 * The CreateProfile method adds a created profile to the the profile database and also assigns the profile to the user.
 * The ReadAll method reads all movies from the database.
 * The MovieDetails method reads the details from a individual movie.
 * The AddMovie method adds a movie object to the database.
 * The EditMovie method edits a existing movie in the database with data provided by the controller.
 * The DeleteMovie method deletes a movie from the database
*/
namespace MovieViewerProject.Services
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbUserRepository(ApplicationDbContext _db, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> ir)
        {
            db = _db;
            userManager = _userManager;
            roleManager = ir;
        }

        public async Task<ApplicationUser> ReadAsync(string userName)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            user.Roles = await userManager.GetRolesAsync(user);
            return user;

        }

        public ApplicationUser Details(string name)
        {
            return db.Users.Include(prof => prof.Profile).ThenInclude(p => p.WatchedCollection)
                .ThenInclude(wm => wm.Movie).FirstOrDefault(p => p.UserName == name);
        }

        public async Task<IQueryable<ApplicationUser>> ReadAllAsync()
        {
            var users = db.Users;

            foreach(var user in users)
            {
                user.Roles = await userManager.GetRolesAsync(user);
            }

            return users;
        }

        public async Task<bool> AssignRoleAsync(string userName, string roleName)
        {
            var user = await ReadAsync(userName);

            if (user != null)
            {
                if (user.HasRole(roleName) == false)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                    
                    return true;
                }
            }

            return false;

        }

        public Profile CreateProfile(string userName, Profile profile)
        {
            var user = Details(userName);

            if(user != null)
            {
                db.Profiles.Add(profile);
                profile.User = user;
                db.SaveChanges();
            }

            return profile;
        }

        public ICollection<Movie> ReadAll()
        {
            return db.Movies.Include(p => p._watched).ToList();
        }

        public Movie MovieDetails(int id)
        {
            return db.Movies.FirstOrDefault(p => p.Id == id);
        }

        public Movie AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
            return movie;
        }

        public void EditMovie(int oldId, Movie movie)
        {
            Movie movieToUpdate = MovieDetails(oldId);
            movieToUpdate.Title = movie.Title;
            movieToUpdate.Year = movie.Year;
            movieToUpdate.Price = movie.Price;
            movieToUpdate.LengthInMinutes = movie.LengthInMinutes;
            movieToUpdate.IMDBUrl = movie.IMDBUrl;
            movieToUpdate._watched = movie._watched;
            db.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            Movie movieToDelete = MovieDetails(id);
            db.Movies.Remove(movieToDelete);
            db.SaveChanges();
        }

       
    }
}
