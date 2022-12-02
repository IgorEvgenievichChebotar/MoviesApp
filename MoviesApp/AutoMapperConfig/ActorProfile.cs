using System.Collections.Generic;
using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.AutoMapperConfig;

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