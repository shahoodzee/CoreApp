using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationCompany
{
    [Key]
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public bool isActive { get; set; }
    public long iCreatedBy { get; set; } = -1;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
