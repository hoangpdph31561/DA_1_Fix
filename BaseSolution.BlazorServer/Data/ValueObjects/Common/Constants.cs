namespace BaseSolution.BlazorServer.Data.ValueObjects.Common
{
    public class QueryConstant
    {
        public class OperatorTypes
        {
            public const string GreaterThan = ">";

            public const string GreaterThanOrEqual = ">=";

            public const string Equal = "=";

            public const string LessThan = "<";

            public const string LessThanOrEqual = "<=";

            public const string None = "";
        }

        public class MatchTypes
        {
            public const bool Contain = false;

            public const bool Equal = true;
        }
    }
}
