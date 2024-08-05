namespace Shared.Domain.Criteria.Filters
{
    public class FilterOperator
    {
        private readonly string? _value = null;
        private FilterOperator(string value)
        {
            _value = value ?? throw new ArgumentNullException("value");
        }


        private const string EQUAL_VALUE = "EQUAL";
        private const string NOT_EQUAL_VALUE = "NOT_EQUAL";
        private const string GT_VALUE = "GT";
        private const string GET_VALUE = "GET";
        private const string LT_VALUE = "LT";
        private const string LET_VALUE = "LET";
        private const string CONTAINS_VALUE = "CONTAINS";
        private const string NOT_CONTAINS_VALUE = "NOT_CONTAINS";
        private const string IN_VALUE = "IN";
        private const string NOT_IN_VALUE = "NOT_IN";
        private const string NONE_VALUE = "NONE";

        public static FilterOperator Equal { get; } = new FilterOperator(EQUAL_VALUE);
        public static FilterOperator NotEqual { get; } = new FilterOperator(NOT_EQUAL_VALUE);
        public static FilterOperator GreaterThan { get; } = new FilterOperator(GT_VALUE);
        public static FilterOperator GreaterEqualThan { get; } = new FilterOperator(GET_VALUE);
        public static FilterOperator LessThan { get; } = new FilterOperator(LT_VALUE);
        public static FilterOperator LessEqualThan { get; } = new FilterOperator(LET_VALUE);
        public static FilterOperator Contains { get; } = new FilterOperator(CONTAINS_VALUE);
        public static FilterOperator NotContains { get; } = new FilterOperator(NOT_CONTAINS_VALUE);
        public static FilterOperator In { get; } = new FilterOperator(IN_VALUE);
        public static FilterOperator NotIn { get; } = new FilterOperator(NOT_IN_VALUE);
        public static FilterOperator None { get; } = new FilterOperator(NONE_VALUE);


        public static FilterOperator Parse(string value)
        {
            Dictionary<string, FilterOperator?> parser = new(){
                {EQUAL_VALUE, Equal},
                {NOT_EQUAL_VALUE, NotEqual},
                {GT_VALUE, GreaterThan},
                {GET_VALUE, GreaterEqualThan },
                {LT_VALUE, LessThan },
                {LET_VALUE, LessEqualThan},
                {CONTAINS_VALUE, Contains},
                {NOT_CONTAINS_VALUE, NotContains},
                {IN_VALUE, In},
                {NOT_IN_VALUE, NotIn},
                {NONE_VALUE, None}
            };

            FilterOperator? status = parser.GetValueOrDefault(value);

            return status ?? throw new ArgumentNullException("value");
        }

        public override string ToString()
        {
            return _value ?? String.Empty;
        }
    }
}