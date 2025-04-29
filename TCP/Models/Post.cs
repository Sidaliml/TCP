namespace TCPCommunication.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key koppling till användare
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
