using System.Collections.Immutable;
using Shared.Domain.Criteria.Filters;
using Shared.Infrastructure.Repository.EntityFramework;
using Shared.Infrastructure.Respository.EntityFramework.Criteria;

namespace Clouds.LastBackups.Infraestructure.Repository.EntityFramework
{
  public static class EntityFrameworkFiltersWrapper<T> where T : Entity
  {
    public static ImmutableList<IFilter<T>> FromDomainFilters(ImmutableList<Filter> filters)
    {
      ImmutableList<IFilter<T>> entityFilterList = [];

      foreach (Filter item in filters)
      {
        IFilter<T>? entityFilter = FromDoaminFilter(item);
        if (null != entityFilter)
          entityFilterList = [.. entityFilterList, entityFilter];
      }

      return entityFilterList;
    }

    private static IFilter<T>? FromDoaminFilter(Filter filter)
    {
      Dictionary<string, Func<string, IFilter<T>>> filterMapper = new Dictionary<string, Func<string, IFilter<T>>> {
        { InMachineIdsFilter<T>.GetFilterName(), (string filterValue) => new InMachineIdsFilter<T>(filterValue)}
      };

      string filterName = filter.field + filter.fieldOperator.ToString();
      Func<string, IFilter<T>>? filterWrapper = filterMapper.GetValueOrDefault(filterName);

      if (null != filterWrapper)
      {
        return filterWrapper(filter.value);
      }
      return null;
    }
  }
}