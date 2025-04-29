namespace TCPCommunication.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public int UserId { get; set; }
    }
}
