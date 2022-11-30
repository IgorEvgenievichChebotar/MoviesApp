using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.AutoMapperConfig;

public class ActorProfile : Profile
{
    protected ActorProfile()
    {
        CreateMap<Actor, ActorViewModel>();
        CreateMap<Actor, InputActorViewModel>().ReverseMap();
        CreateMap<Actor, DeleteActorViewModel>();
        CreateMap<Actor, EditActorViewModel>().ReverseMap();
    }
}