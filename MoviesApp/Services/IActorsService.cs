using System.Collections.Generic;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services;

public interface IActorsService
{
    ICollection<ActorDto> FindAll();
    ActorDto FindById(int id);
    ActorDto Create(ActorDto actorDto);
    ActorDto Update(ActorDto actorDto, int id);
    ActorDto Delete(int id);
}