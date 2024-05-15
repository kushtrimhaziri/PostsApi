namespace PostsApi.Application.Common.Interfaces;

public interface IPostService
{
    Task ImportPosts(byte[] requestFile);
}
