namespace Bookify.Core.ViewModels
{

    public class BookCopyViewModel
    {
        public int Id { get; set; }
    
        public string   BookTitle { get; set; } =null!;

        public bool IsAvilabelForRental { set; get; }
        public int EditionNumber { set; get; }
        public int SerialNumber { set; get; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}