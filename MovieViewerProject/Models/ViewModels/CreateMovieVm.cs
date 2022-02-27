using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.ViewModels
{
    public class CreateMovieVm
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public int Year { get; set; }

        public int LengthInMinutes { get; set; }

        [Required, Range(1, 100)]
        public float Price { get; set; }

        [Required]
        public string IMDBUrl { get; set; }
        public ICollection<Watched> _watched { get; set; }

        public Movie GetMovieInstance()
        {
            return new Movie
            {
               Id = 0,
               Title = this.Title,
               Year = this.Year,
               LengthInMinutes = this.LengthInMinutes,
               Price = this.Price,
               IMDBUrl = this.IMDBUrl,
               _watched = this._watched
               
            };

        }







    }
}
