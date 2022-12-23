using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.Services.Dto.AutoMapperProfiles;

public class DtoProfiles:Profile
{
    public DtoProfiles()
    {
        CreateMap<Actor, ActorDto>().ReverseMap();
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}