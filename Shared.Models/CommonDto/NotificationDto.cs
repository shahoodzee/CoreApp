namespace Shared.Models.CommonDto;

public class NotificationDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public long RecipientId { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public bool isRead { get; set; }
    public DateTime CreatedDate { get; set; }
}
