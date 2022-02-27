using Microsoft.EntityFrameworkCore;
using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the DbMovieRepository. 
 * This class fills out the method stubs from the IMovieRepository.
 * The ReadAll method reads all movies from the database.
 * The Details method returns the movie details of a individual movie.
 * The AddToWatchList method adds a movie to the users watched list, and it adds the user to the movies watched list.
 * The CreateRating method adds a rating to a movie
*/
namespace MovieViewerProject.Services
{
    public class DbMovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProfileRepository profileRepo;

        public DbMovieRepository(ApplicationDbContext db, IProfileRepository profileRepository)
        {
            dbContext = db;
            profileRepo = profileRepository;
        }
        public ICollection<Movie> ReadAll()
        {
            return dbContext.Movies.Include(p => p._watched).ToList();
        }

        public Movie Details(int id)
        {
            return dbContext.Movies.FirstOrDefault(p => p.Id == id);
        }


        public Watched AddToWatchList(int id, Watched watch, int profileId)
        {
            var movie = watch.Movie;
            var profile = profileRepo.Details(profileId);
            
            if(movie != null)
            {
                if (profile != null)
                {
                    movie._watched.Add(watch);
                    profile.WatchedCollection.Add(watch);
                    dbContext.SaveChanges();
                }
            }

            return watch;
        }

        public Rating CreateRating(int id, Rating rating)
        {
            var movie = Details(id);

            if(movie != null)
            {
                movie._rating.Add(rating);
                dbContext.SaveChanges();
            }

            return rating;
        }
    }
}
