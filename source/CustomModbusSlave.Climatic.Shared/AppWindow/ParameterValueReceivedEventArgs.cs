using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class ParameterValueReceivedEventArgs : EventArgs
    {
        public string ParameterId { get; }

        public double Value { get; }

        public ParameterValueReceivedEventArgs(string parameterId, double value) : base()
        {
            ParameterId = parameterId;
            Value = value;
        }
    }
}