

namespace GameZone.Models
{
    public class Category :BaseEntity
    {
        public ICollection<Game> Game { get; set; } = new List<Game>();
    }
}
 