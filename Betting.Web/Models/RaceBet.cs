using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class RaceBet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int RaceListId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Person Person { get; set; }
        public virtual RaceList RaceList { get; set; }
    }
}