using System.Linq.Expressions;

namespace Shared.Infrastructure.Respository.EntityFramework.Criteria
{
  public interface IFilter<T>
  {
    public abstract Expression<Func<T, bool>> ToExpression();
    public static string GetFilterName()
    {
      return "Filter<T>";
    }
  }
}