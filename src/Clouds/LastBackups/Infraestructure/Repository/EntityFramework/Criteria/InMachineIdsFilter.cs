using System.Linq.Expressions;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Domain.ValueObjects;
using Clouds.LastBackups.Infraestructure.Repository.MongoDB;
using Shared.Domain.Criteria.Filters;
using Shared.Infraestructure.Respository.EntityFramework.Criteria;

namespace Clouds.LastBackups.Infraestructure.Repository.EntityFramework
{
  public class InMachineIdsFilter<T> : IFilter<T> where T : Entity
  {
    private readonly List<string> machineIds;


    public InMachineIdsFilter(string value)
    {
      FilterValueList listOfMachineIds = new FilterValueList(value);
      this.machineIds = listOfMachineIds.Value;
    }

    public static string GetFilterName() => MachineId.GetName() + FilterOperator.In.ToString();

    public Expression<Func<T, bool>> ToExpression()
    {
      return backup => this.machineIds.Contains(backup.Id);
    }
  }
}