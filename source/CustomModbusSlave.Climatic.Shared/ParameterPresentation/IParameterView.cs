namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public interface IParameterView
    {
        string GetText(double value);
        string Name { get; }
    }
}