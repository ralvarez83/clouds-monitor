using System.Collections.Immutable;

namespace Shared.Domain.Criteria.Filters
{
    public sealed class Filters
    {
        public ImmutableList<Filter> FiltersFiled { get; private set; } = ImmutableList.Create<Filter>();

        public void Add(Filter newFilter)
        {
            if (!FiltersFiled.Exists((filter) =>
            {
                return filter.field == newFilter.field && filter.fieldOperator == newFilter.fieldOperator;
            }))
            {
                FiltersFiled = FiltersFiled.Add(newFilter);
            }
        }

        public bool HasFilters()
        {
            return FiltersFiled.Count != 0;
        }

    }
}