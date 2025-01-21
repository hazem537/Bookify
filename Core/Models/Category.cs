
namespace Bookify.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public bool IsDeleated { get; set; }
        public DateTime CreatedOnly { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }

    }
}