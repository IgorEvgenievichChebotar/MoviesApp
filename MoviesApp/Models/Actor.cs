using System;
using System.Collections.Generic;

namespace MoviesApp.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}