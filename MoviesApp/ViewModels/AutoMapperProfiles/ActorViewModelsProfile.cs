using AutoMapper;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class ActorViewModelsProfile : Profile
    {
        public ActorViewModelsProfile()
        {
            CreateMap<ActorDto, InputActorViewModel>().ReverseMap();
            CreateMap<ActorDto, DeleteActorViewModel>().ReverseMap();
            CreateMap<ActorDto, EditActorViewModel>().ReverseMap();
            CreateMap<ActorDto, ActorViewModel>().ReverseMap();
        }
    }
}