using AutoMapper;
using PostsApi.Core.Domain;

namespace PostsApi.Application.Posts.Queries.GetPostsWithPagination;

public class PostDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? FriendlyUrl { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
