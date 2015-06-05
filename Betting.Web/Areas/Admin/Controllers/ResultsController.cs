using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Betting.Web.Models;

namespace Betting.Web.Areas.Admin.Controllers
{
    public class ResultsController : Controller
    {
        private BettingContext db = new BettingContext();

        // GET: Admin/Results
        public async Task<ActionResult> Index()
        {
            var results = db.Results.Include(r => r.Competitor).Include(r => r.Race);
            return View(await results.ToListAsync());
        }

        // GET: Admin/Results/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Admin/Results/Create
        public ActionResult Create()
        {
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name");
            return View();
        }

        // POST: Admin/Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RaceId,CompetitorId,Position")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", result.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", result.RaceId);
            return View(result);
        }

        // GET: Admin/Results/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", result.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", result.RaceId);
            return View(result);
        }

        // POST: Admin/Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RaceId,CompetitorId,Position")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompetitorId = new SelectList(db.Competitors, "Id", "Name", result.CompetitorId);
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Name", result.RaceId);
            return View(result);
        }

        // GET: Admin/Results/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Admin/Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Result result = await db.Results.FindAsync(id);
            db.Results.Remove(result);
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
