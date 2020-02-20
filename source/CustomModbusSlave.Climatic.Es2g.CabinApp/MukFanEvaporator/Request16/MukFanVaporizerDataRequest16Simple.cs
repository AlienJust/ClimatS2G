namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16
{
    internal class MukFanVaporizerDataRequest16Simple : IMukFanVaporizerDataRequest16
    {
        public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; set; }
        public int OuterTemperature { get; set; }
        public double InnerTemperature { get; set; }
        public int FanSpeed { get; set; }
        public int DeltaT { get; set; }
        public double DeltaTSetting { get; set; }

        public bool IsSlave { get; set; }
        public bool SalonBit { get; set; }
        public bool FarAwayGoingBit { get; set; }
        public bool HeadWagon { get; set; }
        public bool DoorsAreOpen { get; set; }
        public bool PowerLimitTurningRecycleHeatersOff { get; set; }
    }
}