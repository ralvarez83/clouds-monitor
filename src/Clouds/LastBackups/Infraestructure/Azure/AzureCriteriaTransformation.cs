using Clouds.LastBackups.Domain.ValueObjects;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

namespace Clouds.LastBackups.Infraestructure.Azure
{
  public class AzureCriteriaTransformation(Criteria? criteria = null)
  {
    private const string DATE_FILTER_OPERATOR_EQUAL_GREATER_LESS_THAN = "eq";
    private const string FILTER_CONCATENATION = " and ";

    private Criteria? _criteria = criteria;

    public string GetCriterias()
    {
      string filters = "";

      if (null == _criteria)
        return filters;

      _criteria.filters.FiltersFiled.ForEach(filter =>
      {

        string filterField = filter.field;
        string filterOperator = filter.fieldOperator.ToString();
        string filterValue = filter.value;

        if (BackupDate.GetName() == filter.field)
        {
          filterOperator = DATE_FILTER_OPERATOR_EQUAL_GREATER_LESS_THAN;
          DateTime date = DateTime.Parse(filterValue);
          filterValue = date.ToString("yyyy-MM-dd hh:MM:ss tt");
        }

        if (!string.IsNullOrEmpty(filters))
        {
          filters += FILTER_CONCATENATION;
        }
        filters += $"{filterField} {filterOperator} {filterValue}";
      });

      return filters;
    }

  }
}