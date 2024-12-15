using MediatR;
using Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Core.Commands;

public class ForgetPasswordCommand : IRequest<ApiResponse<bool>>
{
    public string email { get; set; }
    public string? origin { get; set; }
}
