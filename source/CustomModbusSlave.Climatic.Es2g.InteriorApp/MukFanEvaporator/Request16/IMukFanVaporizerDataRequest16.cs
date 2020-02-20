namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16
{
    public interface IMukFanVaporizerDataRequest16
    {
        IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
        int OuterTemperature { get; }
        double InnerTemperature { get; }
        int FanSpeed { get; }
        int DeltaT { get; }
        int HeatingPwmSettingFromSlaveToMaster { get; }
        double DeltaTInDebugMode { get; }
        int PwmUnloadingSetting { get; }
        int PwmUnloadingTime { get; }
        
        /// <summary>
        ///     Секция: мастер или слейв
        /// </summary>
        bool IsSlave { get; }
        bool SalonBit { get; }
        bool FarAwayGoingBit { get; }
        bool HeadWagon { get; }
        bool DoorsAreOpen { get; }
        bool PowerLimitTurningRecycleHeatersOff { get; }
    }
}