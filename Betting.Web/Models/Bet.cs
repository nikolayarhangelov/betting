using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class Bet
    {
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public int CompetitorId { get; set; }

        public virtual User User { get; set; }
        public virtual Race Race { get; set; }
        public virtual Competitor Competitor { get; set; }
    }
}