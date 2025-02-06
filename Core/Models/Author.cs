namespace Bookify.Core.Models
{
public class Author {
            public int ID { get; set; }
            public string Name { get; set; }=null!;
            public DateTime CreatedAt {get; set; } = DateTime.Now;
            public DateTime? UpdatedAt {get; set; }

            public bool IsDeleted { get; set; }=false;

}   

}