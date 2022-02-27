using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the IRoleRepository. 
 * This creates the method stubs for any class that uses the interface.
 * These stubs tell the program what type it needs to return and and types each method can accept.
 * This class contains the method stub to read all the roles in the database.
*/
namespace MovieViewerProject.Services
{
    public interface IRoleRepository
    {
         IQueryable<IdentityRole> ReadAll();

    }
}
