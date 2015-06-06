using System.Linq;
using System.Web.Http;
using Betting.Web.Models;

namespace Betting.Web.ApiControllers
{
    public class RaceController : ApiController
    {
        private readonly BettingContext db = new BettingContext();
        // GET: api/Race/5
        public IQueryable<object> GetRaceLists(int id)
        {
            return db.RaceLists
                .Where(x => x.RaceId == id)
                .Select(x => new {x.Id, x.Position, x.RaceId, x.PersonId, x.Person});
        }
    }
}