using System.Linq.Expressions;
using Hangfire;
using PostsApi.Application.Common.Interfaces;

namespace PostsApi.Persistence.Services;

public class BackgroundJobProcessor : IBackgroundJobProcessor
{
    public void Enqueue(Expression<Action> methodCall)
    {
        BackgroundJob.Enqueue(methodCall);
    }
}
