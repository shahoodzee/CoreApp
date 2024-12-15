﻿using MediatR;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Queries;

public class GetAllCustomersQuery : IRequest<ApiResponse<List<CustomerDto>>>
{
    public long iRoleId { get; set; } = 2;
}