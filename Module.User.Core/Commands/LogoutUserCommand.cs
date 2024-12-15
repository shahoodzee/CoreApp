using MediatR;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Commands;

public class LogoutUserCommand : IRequest<ApiResponse<bool>>
{
    public long userId { get; set; }
}
