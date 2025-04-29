namespace TCPCommunication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Navigation (koppling till deras inlägg och meddelanden)
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
