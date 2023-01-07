using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services;
using MoviesApp.Services.Dto;

namespace MoviesApp.Controllers;

[ApiController]
[Route("/api/actors")]
public class ActorsApiController : ControllerBase
{
    private readonly IActorsService _service;

    public ActorsApiController(IActorsService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<ActorDto>))]
    [Authorize]
    public IActionResult FindAll()
    {
        return Ok(_service.FindAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ActorDto))]
    [ProducesResponseType(204)]
    [Authorize]
    public IActionResult FindById(int id)
    {
        var actor = _service.FindById(id);

        if (actor == null)
        {
            return NoContent();
        }

        return Ok(actor);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ActorDto))]
    /*[ProducesResponseType(400)]*/ //todo
    [Authorize(Roles = "Admin")]
    public IActionResult Create(ActorDto actorDto)
    {
        return Created("FindById", _service.Create(actorDto));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ActorDto))]
    [ProducesResponseType(204)]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(int id, ActorDto actorDto)
    {
        if (_service.FindById(id) == null)
        {
            return NotFound();
        }

        return Ok(_service.Update(actorDto, id));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(200,Type = typeof(ActorDto))]
    [ProducesResponseType(204)]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        if (_service.FindById(id) == null)
        {
            return NoContent();
        }

        return Ok(_service.Delete(id));
    }
}