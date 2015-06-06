using System.Linq;
using System.Web.Http;
using Betting.Web.Models;

namespace Betting.Web.ApiControllers
{
    public class RaceController : ApiController
    {
        private readonly BettingContext db = new BettingContext();
        // GET: api/Race/5
        public IQueryable<RaceList> GetRaceLists(int id)
        {
            return db.RaceLists.Where(x => x.RaceId == id);
        }
    }
}