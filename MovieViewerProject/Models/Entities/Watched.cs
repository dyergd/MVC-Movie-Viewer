using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.Entities
{
    public class Watched
    {
        [Key]
        public int WatchedId  {get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int NumOfTimesWatched { get; set; }

       

        

    }
}
