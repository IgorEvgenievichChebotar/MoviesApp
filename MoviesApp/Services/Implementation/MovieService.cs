using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.Implementation;

public class MovieService : IMovieService
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public MovieService(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MovieDto FindById(int id)
    {
        return _mapper.Map<MovieDto>(_context.Movies.Find(id));
    }

    public ICollection<MovieDto> FindAll()
    {
        return _mapper.Map<ICollection<MovieDto>>(_context.Movies.ToList());
    }

    public MovieDto Update(MovieDto movieDto)
    {
        if (movieDto.Id == null)
        {
            //упрощение для примера
            //лучше всего генерировать ошибки и обрабатывать их на уровне конроллера
            return null;
        }

        try
        {
            var movie = _mapper.Map<Movie>(movieDto);

            _context.Update(movie);
            _context.SaveChanges();

            return _mapper.Map<MovieDto>(movie);
        }
        catch (DbUpdateException)
        {
            if (!_context.Movies.Any(e => e.Id == movieDto.Id))
            {
                //упрощение для примера
                //лучше всего генерировать ошибки и обрабатывать их на уровне конроллера
                return null;
            }
            else
            {
                //упрощение для примера
                //лучше всего генерировать ошибки и обрабатывать их на уровне конроллера
                return null;
            }
        }
    }

    public MovieDto Create(MovieDto movieDto)
    {
        var movie = _context.Add((object)_mapper.Map<Movie>(movieDto)).Entity;
        _context.SaveChanges();
        return _mapper.Map<MovieDto>(movie);
    }

    public MovieDto Delete(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            //упрощение для примера
            //лучше всего генерировать ошибки и обрабатывать их на уровне конроллера
            return null;
        }

        _context.Movies.Remove(movie);
        _context.SaveChanges();

        return _mapper.Map<MovieDto>(movie);
    }
}