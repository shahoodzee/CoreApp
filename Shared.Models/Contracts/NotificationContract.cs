using MongoDB.Bson;

namespace Shared.Models.Contracts;

public class NotificationContract
{
    public ObjectId Id { get; set; }
    public List<long> recipientIds { get; set; }
    public List<string> connectionIds { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public string value { get; set; }
}
