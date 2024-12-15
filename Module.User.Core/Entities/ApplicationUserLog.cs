using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationUserLog
{
    [Key]
    public int Id { get; set; }
    public long UserId { get; set; }
    public DateTime? LoginDateTime { get; set; } = DateTime.UtcNow;
    public DateTime? LogoutDateTime { get; set; }

}
