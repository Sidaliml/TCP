namespace TCPCommunication.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Foreign Key koppling till användare
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
