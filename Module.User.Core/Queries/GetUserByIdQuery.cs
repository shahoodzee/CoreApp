using MediatR;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Queries;

public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
{
    public long userId { get; set; }
}
