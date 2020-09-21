using NCalc;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public sealed class BooleanEvent : IParameterEvent
    {
        private readonly string _expression;
        private readonly bool _invertCondition;

        public string Name { get; }
        public EventLevel Level { get; }

        public BooleanEvent(string expression, string name, bool invertCondition, EventLevel level)
        {
            _expression = expression;
            Name = name;
            _invertCondition = invertCondition;
            Level = level;
        }

        bool CheckForEvent(double value)
        {
            var expr = new Expression(_expression);
            expr.Parameters.Add("value", value);
            bool result = (bool)expr.Evaluate();
            return _invertCondition ? !result : result;
        }
    }
}