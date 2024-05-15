using Microsoft.EntityFrameworkCore;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Core.Domain;
using Slugify;

namespace PostsApi.Persistence.Services;

public class SlugService : ISlugService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;
    private readonly SlugHelper _slugHelper;
    
    public SlugService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.GetRepository<Post>();
        _slugHelper = new SlugHelper();
    }


    public async Task<string> GenerateSlug(string title)
    {
        string slug = _slugHelper.GenerateSlug(title);

        var existingSlug = await (_postRepository.GetByCondition(x=>x.FriendlyUrl == slug)).FirstOrDefaultAsync();
        if (existingSlug == null)
        {
            return slug;
        }

        int count = 1;
        var isSlugUnique = false;

        while (!isSlugUnique)
        {
            var slugExists = (await _postRepository.GetAll()).Any(x => x.FriendlyUrl == $"{slug}-{count}");

            if (!slugExists)
            {
                isSlugUnique = true;
            }
            else
            {
                count++;
            }
        }
        return $"{slug}-{count}";
    }
}
