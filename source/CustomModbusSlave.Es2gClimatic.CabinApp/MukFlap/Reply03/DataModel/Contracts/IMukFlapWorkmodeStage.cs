using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts
{
    internal interface IMukFlapWorkmodeStage
    {
        int AbsoluteValue { get; }
        MukFlapWorkmodeStage KnownValue { get; }
    }
}