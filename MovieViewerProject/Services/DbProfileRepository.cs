using Microsoft.EntityFrameworkCore;
using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the DbProfileRepository. 
 * This class fills out the method stubs from the IProfileRepository.
 * The Details method returns the user profile of a individual user.
 * The Edit method edits a profile object in the database with informatoin provided by the controller.
 * The Delete method deletes a profile from the database.
 * The PayForMovie method adds the amount of money the user spent on the movie to their total,
 * it also checks a field that says the user has paid for a specific movie.
*/
namespace MovieViewerProject.Services
{
    public class DbProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DbProfileRepository(ApplicationDbContext db)
        {
            dbContext = db;
        }


        public Profile Details(int id)
        {
            return dbContext.Profiles.FirstOrDefault(p => p.ProfileId == id);
        }

        public void Edit(int oldId, Profile profile)
        {
            Profile profileToUpdate = Details(oldId);       
            profileToUpdate.CreditCardNumber = profile.CreditCardNumber;
            profileToUpdate.CreditCardExp = profile.CreditCardExp;
            profileToUpdate.StreetAddress = profile.StreetAddress;
            profileToUpdate.State = profile.State;
            profileToUpdate.Zip = profile.Zip;
            profileToUpdate.MoneySpent = profile.MoneySpent;
            profileToUpdate.WatchedCollection = profile.WatchedCollection;
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Profile profileToDelete = Details(id);
            dbContext.Profiles.Remove(profileToDelete);
            dbContext.SaveChanges();
        }

        public void PayForMovie(Movie movie, Profile profile)
        {
            if (movie != null)
            {
                if (profile != null)
                {
                    profile.HasPaid = true;
                    profile.MoneySpent += movie.Price;
                    dbContext.SaveChanges();
                }
            }
        }


     
    }
}
