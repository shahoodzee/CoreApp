using MediatR;
using Shared.Models.Response;

namespace Module.User.Core.Commands;

public class UpdateUserCommand : IRequest<ApiResponse<bool>>
{
    public long userId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string profileImage { get; set; }
    public string phoneNumber { get; set; }
    public int iUserRole { get; set; }
    public long? iModifiedBy { get; set; }
    public int? iCompanyId { get; set; } = -1;
    public string? companyName { get; set; } = "";
}
