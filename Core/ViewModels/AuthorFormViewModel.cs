using Microsoft.AspNetCore.Mvc;

namespace Bookify.Core.ViewModels
{
    public class AuthorFormViewModel
    {
        public int ID { get; set; }

        [Remote(action:"AllowItem",controller:"Authors",ErrorMessage ="this author name is already exist ",AdditionalFields =nameof(ID))]
        public string Name { get; set; } = null!;

    }

}