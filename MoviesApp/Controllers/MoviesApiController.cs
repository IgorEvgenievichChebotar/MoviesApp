using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services;
using MoviesApp.Services.Dto;

namespace MoviesApp.Controllers;

[ApiController]
[Route("/api/movies")]
public class MoviesApiController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesApiController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<MovieDto>))]
    public IActionResult FindAll()
    {
        return Ok(_service.FindAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(MovieDto))]
    [ProducesResponseType(204)]
    public IActionResult FindById(int id)
    {
        var movie = _service.FindById(id);

        if (movie == null)
        {
            return NoContent();
        }

        return Ok(movie);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(MovieDto))]
    /*[ProducesResponseType(400)]*/ //todo
    public IActionResult Create(MovieDto MovieDto)
    {
        return Created("FindById", _service.Create(MovieDto));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(200, Type = typeof(MovieDto))]
    [ProducesResponseType(204)]
    public IActionResult Update(int id, MovieDto movieDto)
    {
        if (_service.FindById(id) == null)
        {
            return NotFound();
        }

        return Ok(_service.Update(movieDto, id));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(200, Type = typeof(MovieDto))]
    [ProducesResponseType(204)]
    public IActionResult Delete(int id)
    {
        if (_service.FindById(id) == null)
        {
            return NoContent();
        }

        return Ok(_service.Delete(id));
    }
}