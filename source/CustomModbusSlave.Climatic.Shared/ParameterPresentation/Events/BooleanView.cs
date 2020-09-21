using NCalc;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public sealed class BooleanEvent : IParameterEvent
    {
        private readonly string _expression;

        public string Name { get; }

        public BooleanEvent(string expression, string name)
        {
            _expression = expression;
            Name = name;
        }

        bool CheckForEvent(double value)
        {
            var expr = new Expression(_expression);
            expr.Parameters.Add("value", value);
            bool result = (bool)expr.Evaluate();
            return result;
        }
    }
}