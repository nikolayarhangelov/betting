using System.Collections.Generic;

namespace Betting.Web.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RaceList> RaceList { get; set; }
    }
}