using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class Competitor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int RaceId { get; set; }

        public virtual Race Race { get; set; }
    }
}