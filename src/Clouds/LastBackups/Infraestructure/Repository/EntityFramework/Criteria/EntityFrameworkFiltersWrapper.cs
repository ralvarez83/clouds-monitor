using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Shared.Domain.Criteria.Filters;
using Shared.Infraestructure.Respository.EntityFramework.Criteria;

namespace Clouds.LastBackups.Infraestructure.Repository.EntityFramework
{
  public static class EntityFrameworkFiltersWrapper
  {
    public static ImmutableList<IFilter<LastBackupStatusDto>> FromDomainFilters(ImmutableList<Filter> filters)
    {
      ImmutableList<IFilter<LastBackupStatusDto>> entityFilterList = [];

      foreach (Filter item in filters)
      {
        IFilter<LastBackupStatusDto>? entityFilter = FromDoaminFilter(item);
        if (null != entityFilter)
          entityFilterList = [.. entityFilterList, entityFilter];
      }

      return entityFilterList;
    }

    private static IFilter<LastBackupStatusDto>? FromDoaminFilter(Filter filter)
    {
      Dictionary<string, Func<string, IFilter<LastBackupStatusDto>>> filterMapper = new Dictionary<string, Func<string, IFilter<LastBackupStatusDto>>> {
        { InMachineIdsFilter.GetFilterName(), (string filterValue) => new InMachineIdsFilter(filterValue)}
      };

      string filterName = filter.field + filter.fieldOperator.ToString();
      Func<string, IFilter<LastBackupStatusDto>>? filterWrapper = filterMapper.GetValueOrDefault(filterName);

      if (null != filterWrapper)
      {
        return filterWrapper(filter.value);
      }
      return null;
    }
  }
}