
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Core.ViewModels
{
    public class BookFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(500)]
        [Remote(action:"AllowItem",controller:"Books",AdditionalFields ="Id,AuthorId",ErrorMessage="Thsi book aready exist")]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        [Required,Display(Name = "Author")]
        [Remote(action:"AllowItem",controller:"Books",AdditionalFields ="Id,Title",ErrorMessage="Thsi book aready exist")]
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }

        [MaxLength(500)]
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; }=DateTime.Now;
        public IFormFile? Image { get; set; }
        [MaxLength(50)]
        public string Hall { get; set; } = null!;
        [Display(Name = "Is avilabel for rental ?")]
        public bool IsAvilabelForRental { set; get; }
        [Display(Name = "Categories")]
        public IList<int> SelectedCategories { get; set; } = new List<int>();

        public IEnumerable<SelectListItem>? Categories { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }


    }
}