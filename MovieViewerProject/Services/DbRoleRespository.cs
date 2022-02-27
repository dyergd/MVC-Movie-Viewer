using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the DbRoleRepository. 
 * This class fills out the method stubs from the IRoleRepository.
 * The ReadAll method is what is called when we want to read all the roles out of the database.
*/

namespace MovieViewerProject.Services
{
    public class DbRoleRepository : IRoleRepository
    {

        private ApplicationDbContext dbContext;

        public DbRoleRepository(ApplicationDbContext db)
        {
            dbContext = db;
        }

        public IQueryable<IdentityRole> ReadAll()
        {
            var roles = dbContext.Roles;
            return roles;
        }
    }

}
