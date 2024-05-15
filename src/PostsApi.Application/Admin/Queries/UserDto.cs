using AutoMapper;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Admin.Queries;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
