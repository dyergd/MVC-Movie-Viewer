using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the IUserRepository. 
 * This creates the method stubs for any class that uses the interface.
 * These stubs tell the program what type it needs to return and and types each method can accept.
 * These class contains method stubs that allow the admin to perform curd operations on movies, and to read and assign roles to users.
*/
namespace MovieViewerProject.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser> ReadAsync(string userName);

        Task<IQueryable<ApplicationUser>> ReadAllAsync();

        Task<bool> AssignRoleAsync(string userName, string roleName);

        ApplicationUser Details(string name);

        Profile CreateProfile(string userName, Profile profile);

        void EditMovie(int oldId, Movie movie);

        void DeleteMovie(int id);

        ICollection<Movie> ReadAll();

        Movie MovieDetails(int id);

        Movie AddMovie(Movie movie);

   

    }
}
