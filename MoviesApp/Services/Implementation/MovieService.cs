using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.Implementation;

public class MovieService : IMovieService
{
    private readonly MoviesContext _moviesContext;
    private readonly IMapper _mapper;

    public MovieService(MoviesContext moviesContext, IMapper mapper)
    {
        _moviesContext = moviesContext;
        _mapper = mapper;
    }

    public MovieDto FindById(int id)
    {
        return _mapper.Map<MovieDto>(_moviesContext.Movies.Find(id));
    }

    public ICollection<MovieDto> FindAll()
    {
        return _mapper.Map<ICollection<MovieDto>>(
            _moviesContext.Movies/*.Include(m => m.Actors)*/
                .ToList());
    }

    public MovieDto Update(MovieDto movieDto, int id)
    {
        var isExists = _moviesContext.Movies.Any(m => m.Id == id);

        if (!isExists)
        {
            return null; //todo
        }

        var movie = _mapper.Map<Movie>(movieDto);
        movie.Id = id;
        _moviesContext.Movies.Update(movie);
        _moviesContext.SaveChanges();
        return _mapper.Map<MovieDto>(movie);
    }

    public MovieDto Create(MovieDto movieDto)
    {
        var movie = _moviesContext.Add((object)_mapper.Map<Movie>(movieDto)).Entity;
        _moviesContext.SaveChanges();
        return _mapper.Map<MovieDto>(movie);
    }

    public MovieDto Delete(int id)
    {
        var movie = _moviesContext.Movies.Find(id);
        if (movie == null)
        {
            //упрощение для примера
            //лучше всего генерировать ошибки и обрабатывать их на уровне конроллера
            return null;
        }

        _moviesContext.Movies.Remove(movie);
        _moviesContext.SaveChanges();

        return _mapper.Map<MovieDto>(movie);
    }
}