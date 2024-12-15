using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationUserGroup
{
    [Key]
    public int Id { get; set; }
    public long ApplicationUserId { get; set; }
    public int ApplicationGroupId { get; set; }
}
