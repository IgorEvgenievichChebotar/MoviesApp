using AutoMapper;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.Models.AutoMapperProfiles;

public class ActorProfile : Profile
{
    public ActorProfile()
    {
        CreateMap<Actor, ActorViewModel>().ReverseMap();
        CreateMap<Actor, InputActorViewModel>().ReverseMap();
        CreateMap<Actor, DeleteActorViewModel>().ReverseMap();
        CreateMap<Actor, EditActorViewModel>().ReverseMap();
    }
}