using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;

namespace PostsApi.Application.Admin.Queries.GetUserByEmail;

[Authorize(Roles = Roles.Administrator)]
public record GetUserByEmailQuery(string email) : IRequest<UserDto>;

public class
    GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery,
    UserDto>
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var userFound = await _adminService.FindByEmail(request.email);

        return _mapper.Map<UserDto>(userFound);
    }
}
