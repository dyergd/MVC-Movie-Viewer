using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
/*This is the ApplicationDbContext. 
  This class creates the dbsets that we use to interact with the database.
*/
namespace MovieViewerProject.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Movie> Movies { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Watched> Watched_ { get; set; }
    }
}
