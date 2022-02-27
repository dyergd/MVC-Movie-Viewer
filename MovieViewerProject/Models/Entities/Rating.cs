using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.Entities
{
    [NotMapped]
    public class Rating
    {
        [Range(1, 10), Display(Name ="Rating")]
        public int RatingNumber { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
