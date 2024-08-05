namespace Shared.Domain.Criteria.Filters
{
    public sealed class Filter
    {
        public static string LAST = "last";
        public static string CLOUD_ID = "cloudId";
        public readonly string field;
        public readonly string value;

        public Filter(string field, string value, FilterOperator fieldOperator)
        {
            this.field = field;
            this.value = value;
            this.fieldOperator = fieldOperator;
        }

        public FilterOperator fieldOperator { get; set; }
    }
}