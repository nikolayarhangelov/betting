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
    public class RaceBetsController : ApiController
    {
        private readonly BettingContext db = new BettingContext();
        // GET: api/RaceBets
        public IQueryable<RaceBet> GetRaceBets()
        {
            return db.RaceBets.Include(x => x.Race).Include(x => x.Person).Include(x => x.RaceList);
        }

        // GET: api/RaceBets/5
        [ResponseType(typeof (RaceBet))]
        public async Task<IHttpActionResult> GetRaceBet(int id)
        {
            var raceBet = await db.RaceBets.FindAsync(id);
            if (raceBet == null)
            {
                return NotFound();
            }

            return Ok(raceBet);
        }

        // PUT: api/RaceBets/5
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutRaceBet(int id, RaceBet raceBet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != raceBet.Id)
            {
                return BadRequest();
            }

            db.Entry(raceBet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceBetExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RaceBets
        [ResponseType(typeof (RaceBet))]
        public async Task<IHttpActionResult> PostRaceBet(RaceBet raceBet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RaceBets.Add(raceBet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = raceBet.Id}, raceBet);
        }

        // DELETE: api/RaceBets/5
        [ResponseType(typeof (RaceBet))]
        public async Task<IHttpActionResult> DeleteRaceBet(int id)
        {
            var raceBet = await db.RaceBets.FindAsync(id);
            if (raceBet == null)
            {
                return NotFound();
            }

            db.RaceBets.Remove(raceBet);
            await db.SaveChangesAsync();

            return Ok(raceBet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RaceBetExists(int id)
        {
            return db.RaceBets.Count(e => e.Id == id) > 0;
        }
    }
}