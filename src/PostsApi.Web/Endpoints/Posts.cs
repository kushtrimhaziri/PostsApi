using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostsApi.Application.Common.Models;
using PostsApi.Application.Posts.Commands.DeletePost;
using PostsApi.Application.Posts.Commands.ImportPosts;
using PostsApi.Application.Posts.Commands.UpdatePost;
using PostsApi.Application.Posts.Queries.GetPostsWithPagination;
using PostsApiW.Infrastructure;
using CreatePostCommand = PostsApi.Application.Posts.Commands.CreatePost.CreatePostCommand;

namespace PostsApiW.Endpoints;

public class Posts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .DisableAntiforgery()
            .MapGet(GetPostsWithPagination)
            .MapPost(CreatePost)
            .MapPost(ImportPosts,"/ImportPosts")
            .MapPut(UpdatePost, "{id}")
            .MapDelete(DeletePost, "{id}");
    }

    public Task<PaginatedList<PostDto>> GetPostsWithPagination(ISender sender,
        [AsParameters] GetPostsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreatePost(ISender sender, CreatePostCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdatePost(ISender sender, int id, UpdatePostCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeletePost(ISender sender, int id)
    {
        await sender.Send(new DeletePostCommand(id));
        return Results.NoContent();
    }
    
    
    public Task<int> ImportPosts(ISender sender, [AsParameters] ImportPostsCommand command)
    {
        return sender.Send(command);
    }
}
