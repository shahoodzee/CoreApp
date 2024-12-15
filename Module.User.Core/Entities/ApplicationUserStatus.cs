using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Entities;

public class ApplicationUserStatus : BaseTable
{
    [Key]
    public int Id { get; set; }
    public string UserStatus { get; set; }
}
