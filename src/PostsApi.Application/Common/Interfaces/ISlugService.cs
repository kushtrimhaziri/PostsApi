namespace PostsApi.Application.Common.Interfaces;

public interface ISlugService
{
    public Task<string> GenerateSlug(string title);
}
