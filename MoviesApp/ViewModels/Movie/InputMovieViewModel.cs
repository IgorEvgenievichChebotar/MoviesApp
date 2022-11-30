using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.ViewModels.Movie
{
    public class InputMovieViewModel
    {
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public ICollection<ActorViewModel> Actors { get; set; } = new List<ActorViewModel>();
    }
}