using System.Linq.Expressions;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Domain.ValueObjects;
using Shared.Domain.Criteria.Filters;
using Shared.Infraestructure.Respository.EntityFramework.Criteria;

namespace Clouds.LastBackups.Infraestructure.Repository.EntityFramework
{
  public class InMachineIdsFilter : IFilter<LastBackupStatusDto>
  {
    private readonly List<string> machineIds;


    public InMachineIdsFilter(string value)
    {
      FilterValueList listOfMachineIds = new FilterValueList(value);
      this.machineIds = listOfMachineIds.Value;
    }

    public static string GetFilterName() => CloudMachineId.GetName() + FilterOperator.In.ToString();

    public Expression<Func<LastBackupStatusDto, bool>> ToExpression()
    {
      return backup => this.machineIds.Contains(backup.MachineId);
    }
  }
}