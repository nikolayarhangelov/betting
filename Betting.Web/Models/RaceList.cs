using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class RaceList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public int PersonId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Person Person { get; set; }
    }
}