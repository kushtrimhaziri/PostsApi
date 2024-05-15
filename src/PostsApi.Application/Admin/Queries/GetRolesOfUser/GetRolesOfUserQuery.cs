using AutoMapper;
using MediatR;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Admin.Queries.GetRolesOfUser;

public record GetRolesOfUserQuery(string userId) : IRequest<IList<string>>;

public class
    GetRolesOfUserQueryHandler : IRequestHandler<GetRolesOfUserQuery,
    IList<string>>
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public GetRolesOfUserQueryHandler(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    public async Task<IList<string>> Handle(GetRolesOfUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _adminService.FindUserById(request.userId);
        if (user != null)
        {
            return await _adminService.GetAllRolesOfUser(_mapper.Map<User>(user));
        }

        return new List<string>();
    }
}
