using Microsoft.AspNetCore.Identity;

namespace Module.User.Core.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileImage { get; set; }
    public int iCompanyId { get; set; }
    public bool isActive { get; set; } = true;
    public long iCreatedBy { get; set; } = -1;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
