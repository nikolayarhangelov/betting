
namespace Betting.Web.Models
{
    public class RaceList
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int PersonId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Person Person { get; set; }
    }
}