using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Betting.Web.Models;

namespace Betting.Web.ApiControllers
{
    public class RaceController : ApiController
    {
        private readonly BettingContext db = new BettingContext();
        // GET: api/Race/5/Lists
        [Route("api/race/{id}/lists")]
        public IQueryable<RaceList> GetRaceLists(int id)
        {
            return db.RaceLists.Where(x => x.RaceId == id).Include(x => x.Race).Include(x => x.Person);
        }

        // GET: api/Race/5/Bets
        [Route("api/race/{id}/bets")]
        public IQueryable<RaceBet> GetRaceBets(int id)
        {
            return db.RaceBets.Where(x => x.RaceId == id).Include(x => x.Person).Include(x => x.RaceList);
        }
    }
}