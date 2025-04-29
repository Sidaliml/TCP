using TCPCommunication.Models;

public class Call
{
    public int Id { get; set; }
    public string Type { get; set; } = "voice";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }       
    public User? User { get; set; }       
}
