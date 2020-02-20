namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03
{
    internal class MukFanEvaporatorWorkstageBuilder : IBuilder<MukFanEvaporatorWorkstage>
    {
        private readonly ushort _data;

        public MukFanEvaporatorWorkstageBuilder(ushort data)
        {
            _data = data;
        }

        public MukFanEvaporatorWorkstage Build()
        {
            switch (_data)
            {
                case 0: return MukFanEvaporatorWorkstage.ControllerInit;
                case 1: return MukFanEvaporatorWorkstage.FanTest;
                case 2: return MukFanEvaporatorWorkstage.WorkAndFanIsGood;
                case 3: return MukFanEvaporatorWorkstage.WorkAndFanIsBad;
                case 4: return MukFanEvaporatorWorkstage.AllSwitchesAndPwmAreCauseNoTemperatureData;
                default: return MukFanEvaporatorWorkstage.Unknown;
            }
        }
    }
}