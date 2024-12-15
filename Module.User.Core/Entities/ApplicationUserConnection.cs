using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationUserConnection
{
    [Key]
    public int Id { get; set; }
    public long UserId { get; set; }
    public string? ConnectionId { get; set; }
    public int iUserStatus { get; set; }

}
