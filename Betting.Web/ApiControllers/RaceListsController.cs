using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Betting.Web.Models;

namespace Betting.Web.ApiControllers
{
    public class RaceListsController : ApiController
    {
        private readonly BettingContext db = new BettingContext();
        // GET: api/RaceLists
        public IQueryable<object> GetRaceLists()
        {
            return db.RaceLists.Select(x => new { x.Id, x.RaceId, x.PersonId });
        }

        // GET: api/RaceLists/5
        [ResponseType(typeof (RaceList))]
        public async Task<IHttpActionResult> GetRaceList(int id)
        {
            var raceList = await db.RaceLists.FindAsync(id);
            if (raceList == null)
            {
                return NotFound();
            }

            return Ok(raceList);
        }

        // PUT: api/RaceLists/5
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutRaceList(int id, RaceList raceList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != raceList.Id)
            {
                return BadRequest();
            }

            db.Entry(raceList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceListExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RaceLists
        [ResponseType(typeof (RaceList))]
        public async Task<IHttpActionResult> PostRaceList(RaceList raceList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RaceLists.Add(raceList);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = raceList.Id}, raceList);
        }

        // DELETE: api/RaceLists/5
        [ResponseType(typeof (RaceList))]
        public async Task<IHttpActionResult> DeleteRaceList(int id)
        {
            var raceList = await db.RaceLists.FindAsync(id);
            if (raceList == null)
            {
                return NotFound();
            }

            db.RaceLists.Remove(raceList);
            await db.SaveChangesAsync();

            return Ok(raceList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RaceListExists(int id)
        {
            return db.RaceLists.Count(e => e.Id == id) > 0;
        }
    }
}