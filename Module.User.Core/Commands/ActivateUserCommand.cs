using MediatR;
using Shared.Models.Response;

namespace Module.User.Core.Commands;

public class ActivateUserCommand : IRequest<ApiResponse<bool>>
{
    public string userId { get; set; }
    public string token { get; set; }
}
