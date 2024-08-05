
namespace Shared.Domain.Criteria;

public sealed class Criteria(Filters.Filters filters, Order.Order? order = null, Pagination? pagination = null)
{
    public readonly Filters.Filters filters = filters;
    public readonly Order.Order? order = order;
    public readonly Pagination? pagination = pagination;

}
