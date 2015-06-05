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
        public int CompetitorId { get; set; }

        [Required]
        public int RaceId { get; set; }

        public virtual User User { get; set; }
        public virtual User Competitor { get; set; }
        public virtual Race Race { get; set; }
    }
}