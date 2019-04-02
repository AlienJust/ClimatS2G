namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    public interface IMukFanVaporizerDataReply03Diagnostic1
    {
        bool FanControllerLinkLost { get; }
        bool FanErrorByDiscreeteInput { get; }
        bool ErrorOneWireSensor1 { get; }
        bool ErrorOneWireSensor2 { get; }
    }
}