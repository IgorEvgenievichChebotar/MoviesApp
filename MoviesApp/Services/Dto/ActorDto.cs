using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.Dto;

public class ActorDto
{
    public int? Id { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Многовато братан")]
    public string Name { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Многовато братан")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [EnsureActorsAge]
    public DateTime BirthDate { get; set; }

    /*public virtual IEnumerable<MovieDto> Movies { get; } = new List<MovieDto>();*/
}