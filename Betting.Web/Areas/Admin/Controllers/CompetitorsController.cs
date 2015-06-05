using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Betting.Web.Models;

namespace Betting.Web.Areas.Admin.Controllers
{
    public class CompetitorsController : Controller
    {
        private readonly BettingContext db = new BettingContext();
        // GET: Admin/Competitors
        public async Task<ActionResult> Index()
        {
            var competitors = db.Competitors.Include(c => c.Race);
            return View(await competitors.ToListAsync());
        }

        // GET: Admin/Competitors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // GET: Admin/Competitors/Create
        public ActionResult Create()
        {
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name");
            return View();
        }

        // POST: Admin/Competitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,RaceId")] Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Competitors.Add(competitor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", competitor.RaceId);
            return View(competitor);
        }

        // GET: Admin/Competitors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", competitor.RaceId);
            return View(competitor);
        }

        // POST: Admin/Competitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RaceId")] Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competitor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", competitor.RaceId);
            return View(competitor);
        }

        // GET: Admin/Competitors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // POST: Admin/Competitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var competitor = await db.Competitors.FindAsync(id);
            db.Competitors.Remove(competitor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}