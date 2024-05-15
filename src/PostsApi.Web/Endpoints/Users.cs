using PostsApi.Core.Domain;
using PostsApiW.Infrastructure;

namespace PostsApiW.Endpoints;

public class Users: EndpointGroupBase
{
    
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<User>();
    }
}
