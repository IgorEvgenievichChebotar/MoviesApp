using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.Controllers;

public class ActorsController : Controller
{
    private readonly MoviesContext _moviesContext;
    private readonly ILogger<ActorsController> _logger;
    private readonly IMapper _mapper;

    public ActorsController(MoviesContext moviesContext, ILogger<ActorsController> logger, IMapper mapper)
    {
        _moviesContext = moviesContext;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var list = _mapper.Map<ICollection<ActorViewModel>>
        (
            _moviesContext.Actors.Include(a => a.Movies)
        );

        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [EnsureActorsAge]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([Bind("Name,LastName,BirthDate")] InputActorViewModel inputModel)
    {
        if (!ModelState.IsValid)
        {
            return View(inputModel);
        }

        _moviesContext.Add((object)_mapper.Map<Actor>(inputModel));
        _moviesContext.SaveChanges();

        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    [Authorize]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<ActorViewModel>(_moviesContext.Actors.FirstOrDefault(a => a.Id == id));


        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editModel = _mapper.Map<EditActorViewModel>(_moviesContext.Actors.FirstOrDefault(a => a.Id == id));

        if (editModel == null)
        {
            return NotFound();
        }

        return View(editModel);
    }

    [HttpPost]
    [EnsureActorsAge]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id, [Bind("Name,LastName,BirthDate")] EditActorViewModel editModel)
    {
        if (!ModelState.IsValid) return View(editModel);
        try
        {
            var actor = _mapper.Map<Actor>(editModel);

            _moviesContext.Update(actor);
            _moviesContext.SaveChanges();
        }
        catch (DbUpdateException)
        {
            if (ActorExists(id))
            {
                throw;
            }

            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deleteModel = _mapper.Map<DeleteActorViewModel>(_moviesContext.Actors.FirstOrDefault(a => a.Id == id));

        if (deleteModel == null)
        {
            return NotFound();
        }

        return View(deleteModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var actor = _moviesContext.Actors.Find(id);
        if (actor == null) return RedirectToAction(nameof(Index));
        _moviesContext.Actors.Remove(actor);
        _moviesContext.SaveChanges();
        _logger.LogError("Actor with id {ActorId} has been deleted!", actor.Id);

        return RedirectToAction(nameof(Index));
    }

    private bool ActorExists(int id)
    {
        return _moviesContext.Actors.Any(a => a.Id == id);
    }
}