using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the IProfileRepository. 
 * This creates the method stubs for any class that uses the interface.
 * These stubs tell the program what type it needs to return and and types each method can accept.
 * This class contains the method stubs for crud operations for profiles, and to pay for movies.
*/
namespace MovieViewerProject.Services
{
    public interface IProfileRepository
    {

        Profile Details(int id); 

        void Edit(int oldId, Profile profile);

        void Delete(int id);

        void PayForMovie(Movie movie, Profile profile);

    }
}
