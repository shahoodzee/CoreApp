using MediatR;
using Shared.Models.Response;

namespace Module.User.Core.Commands;

public class CreateUserCommand : IRequest<ApiResponse<bool>>
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string? profileImage { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string? phoneNumber { get; set; }
    public int iUserRole { get; set; }
    public int iCompanyId { get; set; } = -1;
    public string? companyName { get; set; } = "";
    public string? origin { get; set; }
}
