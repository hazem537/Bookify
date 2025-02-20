namespace Bookify.Core.ViewModels
{
    public class BookCopyFormViewModel
    {

        public int Id { get; set; }

        public int BookID { get; set; }

        public bool IsAvilabelForRental { set; get; }

        [Display(Name="Edition number"), Range(1, 1000,ErrorMessage ="should be between 1 to 1000")]
        public int EditionNumber { set; get; }
        public bool BookForRental { get; set; }
    }
}