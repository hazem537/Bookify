namespace Bookify.Core.Models
{

    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public bool IsAvilabelForRental { set; get; }
        public int EditionNumber { set; get; }
        public int SerialNumber { set; get; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}