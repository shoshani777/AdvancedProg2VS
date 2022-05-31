using AdvancedProgramming2Server.Data;
using AdvancedProgramming2Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming2Server.Controllers
{
    public class RatingsController : Controller
    {
        private readonly AdvancedProgramming2ServerContext _context;

        public RatingsController(AdvancedProgramming2ServerContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            return _context.Rating != null ?
                        View(await _context.Rating.ToListAsync()) :
                        Problem("Entity set 'AdvancedProgramming2ServerContext.Rating'  is null.");
        }
        // GET: Ratings
        public async Task<IActionResult> Search()
        {
            return _context.Rating != null ?
                        View(await _context.Rating.ToListAsync()) :
                        Problem("Entity set 'AdvancedProgramming2ServerContext.Rating'  is null.");
        }
        public async Task<IActionResult> Searched(string query)
        {
            if (_context.Rating == null)
                return Problem("Entity set 'AdvancedProgramming2ServerContext.Rating'  is null.");
            if(query==null || query.Equals(""))
                return PartialView(await _context.Rating.ToListAsync());
            var relevent = _context.Rating.Where(rating => rating.Name.Contains(query) ||
                                                             rating.Review.Contains(query));
            return PartialView(await relevent.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingId,Score,Name,Review")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.Created = DateTime.Now;
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingId,Score,Name,Review")] Rating rating)
        {
            if (id != rating.RatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                rating.Created = DateTime.Now;
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingId))
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
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rating == null)
            {
                return Problem("Entity set 'AdvancedProgramming2ServerContext.Rating'  is null.");
            }
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return (_context.Rating?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }
    }
}
