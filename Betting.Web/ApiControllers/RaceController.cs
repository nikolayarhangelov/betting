using System;
using System.Collections.Generic;
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
            return db.RaceLists
                .Where(x => x.RaceId == id)
                .Include(x => x.Race)
                .Include(x => x.Person);
        }

        // GET: api/Race/5/Lists
        [Route("api/race/{id}/listsdata")]
        public IQueryable<object> GetRaceListsData(int id)
        {
            return db.RaceLists
                .Where(x => x.RaceId == id)
                .Include(x => x.Person)
                .Select(x => new { x.Id, x.Person.Name });
        }

        // GET: api/Race/5/Bets
        [Route("api/race/{id}/bets")]
        public IQueryable<RaceBet> GetRaceBets(int id)
        {
            return db.RaceBets
                .Where(x => x.RaceId == id)
                .Include(x => x.Person)
                .Include(x => x.RaceList);
        }

        // GET: api/Race/5/Score
        [Route("api/race/{id}/score")]
        public IQueryable<RaceScore> GetRaceScore(int id)
        {
            var bets = GetRaceBets(id);
            var results = bets
                .Select(x => x.Person)
                .Distinct()
                .ToDictionary(x => x, x => 0);

            foreach (var bet in bets)
            {
                var actualPosition = bet.RaceList.Position;
                var betPosition = bet.Position;
                var delta = Math.Abs(actualPosition - betPosition);

                switch (delta)
                {
                    case 0:
                        results[bet.Person] += 3;
                        if (actualPosition == 1)
                        {
                            results[bet.Person] += 2;
                        }
                        break;
                    case 1:
                        results[bet.Person] += 1;
                        break;
                }
            }

            return results
                .Select(x => new RaceScore  { Id = x.Key.Id, Name = x.Key.Name, Score = x.Value })
                .AsQueryable();
        }

        public class RaceScore
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}