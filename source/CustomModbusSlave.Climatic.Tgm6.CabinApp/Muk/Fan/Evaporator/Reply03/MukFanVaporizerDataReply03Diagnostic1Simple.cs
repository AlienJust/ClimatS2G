namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    internal class MukFanVaporizerDataReply03Diagnostic1Simple : IMukFanVaporizerDataReply03Diagnostic1
    {
        public bool FanControllerLinkLost { get; set; }
        public bool FanErrorByDiscreteInput { get; set; }
        public bool ErrorOneWireSensor1 { get; set; }
        public bool ErrorOneWireSensor2 { get; set; }
    }
}