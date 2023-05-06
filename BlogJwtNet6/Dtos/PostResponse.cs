namespace BlogJwtNet6.Dtos
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserResponse Author { get; set; }
    }
}
