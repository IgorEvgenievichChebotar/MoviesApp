using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels.Actor;

namespace MoviesApp.Controllers;

public class ActorsController : Controller
{
    private readonly MoviesContext _context;
    private readonly ILogger<ActorsController> _logger;
    private readonly IMapper _mapper;

    public ActorsController(MoviesContext context, ILogger<ActorsController> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var list = _mapper.Map<ICollection<ActorViewModel>>
        (
            _context.Actors.Include(a => a.Movies)
        );

        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([Bind("Name,LastName,BirthDate")] InputActorViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(_mapper.Map<Actor>(inputModel));
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return View(inputModel);
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<ActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));


        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editModel = _mapper.Map<EditActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));

        if (editModel == null)
        {
            return NotFound();
        }

        return View(editModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Name,LastName,BirthDate")] EditActorViewModel editModel)
    {
        if (!ModelState.IsValid) return View(editModel);
        try
        {
            var actor = _mapper.Map<Actor>(editModel);

            _context.Update(actor);
            _context.SaveChanges();
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
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deleteModel = _mapper.Map<DeleteActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));

        if (deleteModel == null)
        {
            return NotFound();
        }

        return View(deleteModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var actor = _context.Actors.Find(id);
        if (actor == null) return RedirectToAction(nameof(Index));
        _context.Actors.Remove(actor);
        _context.SaveChanges();
        _logger.LogError("Actor with id {ActorId} has been deleted!", actor.Id);

        return RedirectToAction(nameof(Index));
    }

    private bool ActorExists(int id)
    {
        return _context.Actors.Any(a => a.Id == id);
    }
}