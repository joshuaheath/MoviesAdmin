using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class CriticReviewsController : Controller
{
    private readonly MoviesContext _context;

    public CriticReviewsController(MoviesContext context)
    {
        _context = context;
    }

    // GET: CriticReviews
    public async Task<IActionResult> Index()
    {
        return View(await _context.CriticReview.ToListAsync());
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
    public IActionResult Create()
    {
        return View();
    }

    // POST: CriticReviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,description,rating,isPublished,createdBy,createdDate")] CriticReview criticReview)
    {
        if (ModelState.IsValid)
        {
            _context.Add(criticReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(criticReview);
    }

    // GET: CriticReviews/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticReview = await _context.CriticReview.FindAsync(id);
        if (criticReview == null)
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
        if (id != criticReview.id)
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
