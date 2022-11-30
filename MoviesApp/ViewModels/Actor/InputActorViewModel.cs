using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesApp.ViewModels.Movie;

namespace MoviesApp.ViewModels.Actor;

public class InputActorViewModel
{
    public string Name { get; set; }
    public string LastName { get; set; }

    [DataType(DataType.Date)] 
    public DateTime BirthDate { get; set; }
    public virtual ICollection<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
}