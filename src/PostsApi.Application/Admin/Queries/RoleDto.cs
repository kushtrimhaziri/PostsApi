using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace PostsApi.Application.Admin.Queries;

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoleDto, IdentityRole>().ReverseMap();
        }
    }
}


