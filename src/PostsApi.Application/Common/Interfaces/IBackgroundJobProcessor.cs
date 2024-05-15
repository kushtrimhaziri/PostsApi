using System.Linq.Expressions;

namespace PostsApi.Application.Common.Interfaces;

public interface IBackgroundJobProcessor
{
    void Enqueue(Expression<Action> methodCall);
}
