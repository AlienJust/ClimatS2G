using CustomModbusSlave.Es2gClimatic.Shared.EmersionDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03
{
    public interface IMukFanVaporizerDataReply03
    {
        ushort FanPwm { get; }
        ISensorIndication<double> TemperatureAddress1 { get; }
        ISensorIndication<double> TemperatureAddress2 { get; }
        byte IncomingSignals { get; }
        byte OutgoingSignals { get; }

        ushort AnalogInput { get; }
        ushort HeatingPwm { get; } // TODO: rename (was taken from Warm Floor Muk)

        ushort AutomaticModeStage { get; }
        MukFanEvaporatorWorkstage AutomaticModeStageParsed { get; }

        ISensorIndication<double> TemperatureAddress3 { get; }
        ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode { get; }
        double CalculatedTemperatureSetting { get; }
        ushort FanSpeed { get; }
        ushort Diagnostic1 { get; }
        IMukFanVaporizerDataReply03Diagnostic1 Diagnostic1Parsed { get; }

        ushort Diagnostic2 { get; }
        IMukFanVaporizerDataReply03Diagnostic2 Diagnostic2Parsed { get; }

        ushort Diagnostic3 { get; }
        IMukEmersionSwitchOnDiagnostic Diagnostic3Parsed { get; }

        ushort Diagnostic4 { get; }
        IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire1 { get; }

        ushort Diagnostic5 { get; }
        IMukFlapDiagnosticOneWireSensor Diagnostic5OneWire2 { get; }

        ushort FirmwareBuildNumber { get; }
    }
}