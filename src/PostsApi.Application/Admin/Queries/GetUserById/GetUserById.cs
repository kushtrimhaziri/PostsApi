using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostsApi.Application.Admin.Queries.GetUserByEmail;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Constants;

namespace PostsApi.Application.Admin.Queries.GetUserById;



[Authorize(Roles = Roles.Administrator)]
public record GetUserByIdQuery(string id) : IRequest<UserDto>;

public class
    GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery,
    UserDto>
{
    private readonly IAdminService _adminService;

    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var userFound = await _adminService.FindUserById(request.id);

        return _mapper.Map<UserDto>(userFound);
    }
}
