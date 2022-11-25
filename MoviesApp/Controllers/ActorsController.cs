using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers;

public class ActorsController : Controller
{
    private readonly MoviesContext _context;
    private readonly ILogger<HomeController> _logger;

    public ActorsController(MoviesContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var list = _context.Actors
            .Include(a => a.Movies)
            .Select(a => new ActorViewModel
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
                Movies = a.Movies
            })
            .AsEnumerable();

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
            _context.Add(new Actor
            {
                Name = inputModel.Name,
                LastName = inputModel.LastName,
                BirthDate = inputModel.BirthDate
            });
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

        var viewModel = _context.Actors.Where(a => a.Id == id).Select(a => new ActorViewModel()
        {
            Id = a.Id,
            Name = a.Name,
            LastName = a.LastName,
            BirthDate = a.BirthDate
        }).FirstOrDefault();


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

        var editModel = _context.Actors.Where(a => a.Id == id).Select(a => new EditActorViewModel()
        {
            Name = a.Name,
            LastName = a.LastName,
            BirthDate = a.BirthDate
        }).FirstOrDefault();

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
            var actor = new Actor()
            {
                Id = id,
                Name = editModel.Name,
                LastName = editModel.LastName,
                BirthDate = editModel.BirthDate
            };

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

        var deleteModel = _context.Actors.Where(a => a.Id == id).Select(a => new DeleteActorViewModel()
        {
            Name = a.Name,
            LastName = a.LastName,
            BirthDate = a.BirthDate
        }).FirstOrDefault();

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