namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Request
{
    public interface IRequestF0
    {
        int FrequencySetting { get; }
        bool TurnOnCommand { get; }
    }
}