
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

        List<Movie> movies = await _context.Movie
            .Include(m => m.genre)
            .Include(m => m.audienceRating)
            .OrderByDescending(m => m.releaseDate)
            .ToListAsync();
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
            .Include(m => m.genre) //populate the nav property
            .FirstOrDefaultAsync(m => m.id == id); // get trail from db where Id = 5

        if (movie == null)
        {
            return NotFound();
        }


        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        //Create a select list for the genres
        var genres = _context.Genre.ToList();
        ViewBag.genreid = new SelectList(genres, "id", "title");

        //Create a select list for the AudienceRating
        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.audienceRatingid = new SelectList(audienceRating, "id", "title");

        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,runtimeMin,title,synopsis,releaseDate,imageUrl,genreid,audienceRatingid")] Movie movie, IFormFile ImageUpload)
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

        //Create a select list for the genres
        var genres = _context.Genre.ToList();
        ViewBag.genreid = new SelectList(genres, "id", "title");

        //Create a select list for the AudienceRating
        var audiences = _context.AudienceRating.ToList();
        ViewBag.audienceRatingid = new SelectList(audiences, "id", "title");

        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,runtimeMin,title,synopsis,releaseDate,imageUrl,genreid,audienceRatingid")] Movie movie)
    {
        if (id != movie.id)
        {
            return NotFound();
        }
         var exists = _context.AudienceRating.Any(a => a.id == movie.audienceRatingid);
         if (!exists)
         {
             ModelState.AddModelError("audienceRatingid", "Selected Audience Rating does not exist.");
             // repopulate ViewBag and return View(movie);
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

        //Create a select list for the genres
        var genres = _context.Genre.ToList();
        ViewBag.genreid = new SelectList(genres, "id", "title");

        //Create a select list for the AudienceRating
        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.audienceRatingid = new SelectList(audienceRating, "id", "title");

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
