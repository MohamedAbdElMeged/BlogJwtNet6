using System.ComponentModel.DataAnnotations;

namespace BlogJwtNet6.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Body { get; set; }

        public  User Author { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
