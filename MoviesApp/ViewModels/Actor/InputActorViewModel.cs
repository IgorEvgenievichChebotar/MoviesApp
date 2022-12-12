using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;
using MoviesApp.ViewModels.Movie;

namespace MoviesApp.ViewModels.Actor;

public class InputActorViewModel
{
    [ShortNameValidation]
    public string Name { get; set; }
    [ShortNameValidation]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    public virtual ICollection<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
}