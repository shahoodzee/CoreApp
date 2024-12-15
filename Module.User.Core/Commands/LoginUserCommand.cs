using MediatR;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace Module.User.Core.Commands;

public class LoginUserCommand : IRequest<ApiResponse<LoginDto>>
{
    [Required]
    public string email { get; set; }

    [Required]
    public string password { get; set; }
}
