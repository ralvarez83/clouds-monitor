using System.Linq.Expressions;

namespace Shared.Infraestructure.Respository.EntityFramework.Criteria
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