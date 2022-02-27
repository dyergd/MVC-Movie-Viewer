using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the IMovieRepository. 
 * This creates the method stubs for any class that uses the interface.
 * These stubs tell the program what type it needs to return and and types each method can accept.
 * This class contains the method stubs for crud operations for movies, and to add movies to a watch list.
*/
namespace MovieViewerProject.Services
{
    public interface IMovieRepository
    {
        ICollection<Movie> ReadAll();

        Movie Details(int id);

        Watched AddToWatchList(int id, Watched watch, int profileId);

        Rating CreateRating(int id, Rating rating);

        

    }
}
