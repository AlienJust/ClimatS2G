namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Request
{
    class RequestF0Simple : IRequestF0
    {
        public RequestF0Simple(int frequencySetting, bool turnOnCommand)
        {
            FrequencySetting = frequencySetting;
            TurnOnCommand = turnOnCommand;
        }

        public int FrequencySetting { get; }
        public bool TurnOnCommand { get; }
    }
}