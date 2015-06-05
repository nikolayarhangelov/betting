using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class Result
    {
        public int Id { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public int CompetitorId { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Race Race { get; set; }
        public virtual Competitor Competitor { get; set; }
    }
}