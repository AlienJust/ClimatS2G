namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03
{
    public interface IMukFanVaporizerDataReply03Diagnostic1
    {
        bool FanControllerLinkLost { get; }
        bool FanErrorByDiscreeteInput { get; }
        bool ErrorOneWireSensor1 { get; }
        bool ErrorOneWireSensor2 { get; }
    }
}