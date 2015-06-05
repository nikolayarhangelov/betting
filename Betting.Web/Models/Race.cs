using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class Race
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}