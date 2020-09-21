using System.Collections.Generic;
namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public interface IParameterDescription
    {
        string Key { get; }
        string Identifier { get; }

        string CustomName { get; }

        IParameterView View { get; }

        IList<IParameterEvent> Events { get; }

        IParameterInjectionConfiguration Injection { get; }
    }

    public interface IParameterEvent
    {
        string Name { get; }
        bool CheckForEvent(double value);

        EventLevel Level { get; }
    }

    public enum EventLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical,
        Fatal
    }
}