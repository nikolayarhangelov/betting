using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}