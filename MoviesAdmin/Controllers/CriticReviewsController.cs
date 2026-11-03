using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin;
using MoviesAdmin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class CriticReviewsController : Controller
{
    private readonly MoviesContext _context;
    private string _CurrentUserId => User.GetUserId();

    private bool CanManageReview(CriticReview review) // Helper functions for admins and critics who own the reviews
    {
        return review.createdBy == _CurrentUserId || User.IsInRole("admin");
    }

    public CriticReviewsController(MoviesContext context)
    {
        _context = context;
    }

    //public override void OnActionExecuted(ActionExecutedContext context)
    //{
    //    base.OnActionExecuted(context);
    //    _userId = User.GetUserId();
    //}

    // GET: CriticReviews
    public async Task<IActionResult> Index()
    {
        string localUserId = User.GetUserId();
        var movies = await _context.CriticReview
            .Include(review => review.movie)
            .ToListAsync();
        return View(movies);
    }


    // GET: CriticOwnReviews
    public async Task<IActionResult> CriticOwnReviews()
    {
            var movies = await _context.CriticReview
                .Where(review => review.createdBy == _CurrentUserId)
                .Include(review => review.movie)
                .ToListAsync();
            return View(movies);
        
    }

    // GET: CriticReviews/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticReview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.id == id);
        if (criticReview == null)
        {
            return NotFound();
        }

        return View(criticReview);
    }

    // GET: CriticReviews/Create
    public async Task<IActionResult> Create()
    {
        await PopulateMovieList();
        return View();
    }


    //Critics will only need the following feilds to make a review
    // movieId, description, rating, isPublished
    // POST: CriticReviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,movieId,description,rating,isPublished")] CriticReview criticReview)
    {
        // Do not let the critic to submit as someone else
        criticReview.createdBy = User.GetUserId();
        criticReview.createdDate = DateTime.Now;

        // Because createdBy is not supplied by the form.
        ModelState.Remove(nameof(CriticReview.createdBy));


        //if (!await _context.Movie.AnyAsync(movie => movie.id == criticReview.id))
        //{
        //    ModelState.AddModelError(nameof(CriticReview.movieId), "Please select a valid movie");
        //}

        //Uploads valid review to database
        if (ModelState.IsValid)
        {
            _context.CriticReview.Add(criticReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(criticReview);
    }

    private async Task PopulateMovieList(int? selectedMovieId = null)
    {
        var movies = await _context.Movie
            .OrderBy(movie => movie.title)
            .ToListAsync();

        ViewBag.movieId = new SelectList(
            movies,
            "id",
            "title",
            selectedMovieId);
    }

    // GET: CriticReviews/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticReview = await _context.CriticReview.FindAsync(id);
        if (criticReview == null || !CanManageReview(criticReview)) // Does not let user get information to edit unless its the same Critic who made it
        {
            return NotFound();
        }
        return View(criticReview);
    }

    // POST: CriticReviews/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("id,description,rating,isPublished,createdBy,createdDate")] CriticReview criticReview)
    {
        var existingReview = await _context.CriticReview.FindAsync(id); //Finds the exsisting movie using ID insead of inputed values
        if (id != criticReview.id || !CanManageReview(criticReview))
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(criticReview);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriticReviewExists(criticReview.id))
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
        return View(criticReview);
    }

    // GET: CriticReviews/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {

        var criticReview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.id == id);

        if (id == null || !CanManageReview(criticReview))
        {
            return NotFound();
        }
        if (criticReview == null)
        {
            return NotFound();
        }

        return View(criticReview);
    }

    // POST: CriticReviews/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var criticReview = await _context.CriticReview.FindAsync(id);
        if (criticReview != null)
        {
            _context.CriticReview.Remove(criticReview);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CriticReviewExists(int id)
    {
        return _context.CriticReview.Any(e => e.id == id);
    }


}
