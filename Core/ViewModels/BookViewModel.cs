namespace Bookify.Core.ViewModels
{
    class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        public string Hall { get; set; } = null!;
        public bool IsAvilabelForRental { set; get; }

        public IEnumerable<string> Categories { get; set; } = null!;
        public IEnumerable<BookCopyViewModel> Copies { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }


    }
}