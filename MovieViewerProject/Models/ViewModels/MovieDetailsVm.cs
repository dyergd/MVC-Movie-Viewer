using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.ViewModels
{
    public class MovieDetailsVm
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Display(Name = "Length In Minutes")]
        public int LengthInMinutes { get; set; }

        [Required, Range(1, 100)]
        public float Price { get; set; }

        [Required, Display(Name = "Movie Url")]
        public string IMDBUrl { get; set; }

        public ICollection<Watched> _watched { get; set; }

        public ICollection<Rating> _rating { get; set; } 

        public MovieDetailsVm GetMovieDetails() 
        {
            return new MovieDetailsVm
            {
                Id = this.Id,
                Title = this.Title,
                Year = this.Year,
                Price = this.Price,
                LengthInMinutes = this.LengthInMinutes,
                IMDBUrl = this.IMDBUrl,
                _watched = this._watched,
                _rating = this._rating

            };
        }


    }
}
