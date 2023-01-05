using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.Implementation;

public class ActorsService : IActorsService
{
    private readonly MoviesContext _moviesContext;
    private readonly IMapper _mapper;

    public ActorsService(IMapper mapper, MoviesContext moviesContext)
    {
        _mapper = mapper;
        _moviesContext = moviesContext;
    }

    public ICollection<ActorDto> FindAll()
    {
        return _mapper.Map<ICollection<ActorDto>>(
            _moviesContext.Actors/*.Include(a => a.Movies)*/
                .ToList());
    }

    public ActorDto FindById(int id)
    {
        return _mapper.Map<ActorDto>(_moviesContext.Actors.Find(id));
    }

    public ActorDto Create(ActorDto actorDto)
    {
        var actor = _moviesContext.Actors.Add(_mapper.Map<Actor>(actorDto)).Entity;
        _moviesContext.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }

    public ActorDto Update(ActorDto actorDto, int id)
    {
        var isExists = _moviesContext.Actors.Any(a => a.Id == id);

        if (!isExists)
        {
            return null; //todo
        }

        var actor = _mapper.Map<Actor>(actorDto);
        actor.Id = id;
        _moviesContext.Actors.Update(actor);
        _moviesContext.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }

    public ActorDto Delete(int id)
    {
        var actor = _moviesContext.Actors.Find(id);

        if (actor == null)
        {
            return null; //todo
        }

        _moviesContext.Actors.Remove(actor);
        _moviesContext.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }
}