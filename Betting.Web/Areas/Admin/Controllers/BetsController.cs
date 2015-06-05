using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Betting.Web.Models;

namespace Betting.Web.Areas.Admin.Controllers
{
    public class BetsController : Controller
    {
        private readonly BettingContext db = new BettingContext();
        // GET: Admin/Bets
        public async Task<ActionResult> Index()
        {
            var bets = db.Bets.Include(b => b.Competitor).Include(b => b.Race).Include(b => b.User);
            return View(await bets.ToListAsync());
        }

        // GET: Admin/Bets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bet = await db.Bets.FindAsync(id);
            if (bet == null)
            {
                return HttpNotFound();
            }
            return View(bet);
        }

        // GET: Admin/Bets/Create
        public ActionResult Create()
        {
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Admin/Bets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Position,UserId,RaceId,CompetitorId")] Bet bet)
        {
            if (ModelState.IsValid)
            {
                db.Bets.Add(bet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", bet.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", bet.RaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bet.UserId);
            return View(bet);
        }

        // GET: Admin/Bets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bet = await db.Bets.FindAsync(id);
            if (bet == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", bet.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", bet.RaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bet.UserId);
            return View(bet);
        }

        // POST: Admin/Bets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Position,UserId,RaceId,CompetitorId")] Bet bet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", bet.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", bet.RaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bet.UserId);
            return View(bet);
        }

        // GET: Admin/Bets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bet = await db.Bets.FindAsync(id);
            if (bet == null)
            {
                return HttpNotFound();
            }
            return View(bet);
        }

        // POST: Admin/Bets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var bet = await db.Bets.FindAsync(id);
            db.Bets.Remove(bet);
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