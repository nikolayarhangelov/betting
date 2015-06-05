using System.ComponentModel.DataAnnotations;

namespace Betting.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}