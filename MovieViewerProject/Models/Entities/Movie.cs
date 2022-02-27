using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.Entities
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Display(Name = "Length In Minutes")]
        public int LengthInMinutes { get; set; }

        [Required, Range(1,100)]
        public float Price { get; set; }

        [Required, Display(Name = "Movie URL")]
        public string IMDBUrl { get; set; }

        
        public ICollection<Watched> _watched { get; set; } = new List<Watched>();
        
        public ICollection<Rating> _rating { get; set; } = new List<Rating>();
          

    }
}
