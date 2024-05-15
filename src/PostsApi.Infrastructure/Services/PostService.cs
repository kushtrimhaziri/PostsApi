using System.Globalization;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Application.Posts.Queries.GetPostsWithPagination;
using PostsApi.Core.Domain;

namespace PostsApi.Persistence.Services;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;
    private readonly IMapper _mapper;
    
    public PostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _postRepository = _unitOfWork.GetRepository<Post>();
    }

    public async Task ImportPosts(byte[] requestFile)
    {
        if (requestFile != null)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            
            using var memoryStream = new MemoryStream(requestFile);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, config);

            var postsToBeImported = csv.GetRecords<PostDto>();

            foreach (var postToBeImported in postsToBeImported)
            {
                var entity = new Post
                {
                    Title = postToBeImported.Title,
                    Content = postToBeImported.Content,
                    FriendlyUrl = postToBeImported.FriendlyUrl
                };

                await _postRepository.Add(entity);
            }

            _unitOfWork.Save();
        }
    }
}
