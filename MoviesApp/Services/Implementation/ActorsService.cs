using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.Implementation;

public class ActorsService : IActorsService
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public ActorsService(IMapper mapper, MoviesContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public ICollection<ActorDto> FindAll()
    {
        return _mapper.Map<ICollection<ActorDto>>(_context.Actors.ToList());
    }

    public ActorDto FindById(int id)
    {
        return _mapper.Map<ActorDto>(_context.Actors.Find(id));
    }

    public ActorDto Create(ActorDto actorDto)
    {
        var actor = _context.Actors.Add(_mapper.Map<Actor>(actorDto)).Entity;
        _context.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }

    public ActorDto Update(ActorDto actorDto, int id)
    {
        var isExists = _context.Actors.Any(a => a.Id == id);
        
        if (!isExists)
        {
            return null; //todo
        }

        var actor = _mapper.Map<Actor>(actorDto);
        actor.Id = id;
        _context.Actors.Update(actor);
        _context.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }

    public ActorDto Delete(int id)
    {
        var actor = _context.Actors.Find(id);

        if (actor == null)
        {
            return null; //todo
        }

        _context.Actors.Remove(actor);
        _context.SaveChanges();
        return _mapper.Map<ActorDto>(actor);
    }
}