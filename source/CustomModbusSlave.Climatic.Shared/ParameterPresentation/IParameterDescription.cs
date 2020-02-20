namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public interface IParameterDescription
    {
        string Key { get; }
        string Identifier { get; }

        string CustomName { get; }

        IParameterView View { get; }

        IParameterInjectionConfiguration Injection { get; }
    }
}