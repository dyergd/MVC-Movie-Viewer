using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieViewerProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public ICollection<string> Roles { get; set; }

        public Profile Profile { get; set; }

        public ApplicationUser()
        {
            Roles = new List<string>();
        }

        public bool HasRole(string roleName)
        {
            return Roles.Any(r => r == roleName);
        }

    }
}
