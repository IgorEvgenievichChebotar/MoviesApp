using System.Collections.Generic;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services;

public interface IMovieService
{
    MovieDto FindById(int id);
    ICollection<MovieDto> FindAll();
    MovieDto Update(MovieDto movieDto);
    MovieDto Create(MovieDto movieDto);
    MovieDto Delete(int id);
}