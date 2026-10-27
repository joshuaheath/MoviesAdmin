
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MoviesAdmin;

[Authorize]
public class MoviesController : Controller
{
    private readonly MoviesContext _context;

    private readonly AzureStorageService _azureStorage;

    public MoviesController(MoviesContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: MOVIES
    //Uses LINQ methods to sort
    public async Task<IActionResult> Index()    
    {
        //Await means that this can be preformed while other things are happening
        var movies = await _context.Movie
            //for each movie, sort by property releaseDate, releaseDate being the key to sort by
            .OrderByDescending(m => m.releaseDate)
            //Makes the query to the database run and puts the result into a list using that method above
            .ToListAsync();
        User.GetUserId();
        return View(movies);
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,runtimeMin,title,synopsis,releaseDate,imageUrl")] Movie movie, IFormFile ImageUpload)
    {
        if (ImageUpload == null) {
            ModelState.AddModelError("ImageUpload", "Image file is required");
            
        }


        if (ModelState.IsValid)
        {

            string extension = Path.GetExtension(ImageUpload.FileName); // includes the leading dot, e.g. ".jpg"
            string newFileName = $"{Guid.NewGuid()}{extension}";

            // Upload file to Azure storage
            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);


            movie.imageUrl = url;

            //Save trail to data base

            _context.Add(movie); // add new trail to context (db)
            await _context.SaveChangesAsync(); // Save changes in dp
            return RedirectToAction(nameof(Index)); // re-direct to
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,runtimeMin,title,synopsis,releaseDate,imageUrl")] Movie movie)
    {
        if (id != movie.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {


            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.id == id);
    }
}
