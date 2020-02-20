namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts
{
    interface IMukFlapDiagnostic1
    {
        bool NoEmersionControllerAnswer { get; }
        bool SensorOneWire1Error { get; }
        bool SensorOneWire2Error { get; }
    }
}