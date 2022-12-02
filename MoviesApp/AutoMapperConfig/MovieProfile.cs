using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels.Movie;

namespace MoviesApp.AutoMapperConfig;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieViewModel>().ReverseMap();
        CreateMap<Movie, InputMovieViewModel>().ReverseMap();
        CreateMap<Movie, DeleteMovieViewModel>().ReverseMap();
        CreateMap<Movie, EditMovieViewModel>().ReverseMap();
    }
}