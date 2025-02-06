using Microsoft.EntityFrameworkCore;

namespace Bookify.Core.Models
{
    [Index(nameof(Title) ,nameof(AuthorId),IsUnique = true)]
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [MaxLength(500)]
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        [MaxLength(50)]
        public string Hall { get; set; } = null!;
        public bool IsAvilabelForRental { set; get; }
        
        public ICollection<BookCategory> Categories { get; set; } = new List<BookCategory>();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }


    }
}