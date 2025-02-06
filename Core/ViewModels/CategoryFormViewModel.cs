using Microsoft.AspNetCore.Mvc;

namespace Bookify.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Remote(action: "AllowItem", controller: "Categories", ErrorMessage = "this name is already exist ",AdditionalFields =nameof(Id))]
        [Required,StringLength(100)]
        public string Name { get; set; } = null!;
    }
}