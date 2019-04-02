using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.EmersionDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;
using TemperatureRegulatorWorkModeBuilderReplied = CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.TemperatureRegulatorWorkMode.TemperatureRegulatorWorkModeBuilderReplied;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    internal class MukFanVaporizerDataReply03Builder : IBuilder<IMukFanVaporizerDataReply03>
    {
        private static readonly BytesPair NoSensor = new BytesPair(0x85, 0x00);
        private readonly IList<byte> _data;

        public MukFanVaporizerDataReply03Builder(IList<byte> bytes)
        {
            _data = bytes;
        }

        public IMukFanVaporizerDataReply03 Build()
        {
            var automaticModeStage = new BytesPair(_data[17], _data[18]).HighFirstUnsignedValue;

            return new MukFanVaporizerDataReply03Simple
            {
                FanPwm = new BytesPair(_data[3], _data[4]).HighFirstUnsignedValue,

                TemperatureAddress1 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[5], _data[6]), 0.01, 0.0, NoSensor),
                TemperatureAddress2 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[7], _data[8]), 0.01, 0.0, NoSensor),

                IncomingSignals = _data[10],
                OutgoingSignals = _data[12],

                AnalogInput = new BytesPair(_data[13], _data[14]).HighFirstUnsignedValue,
                FlapPwm = new BytesPair(_data[15], _data[16]).HighFirstUnsignedValue,

                AutomaticModeStage = automaticModeStage,
                AutomaticModeStageParsed = new MukFanEvaporatorWorkstageBuilder(automaticModeStage).Build(),


                TemperatureRegulatorWorkMode = new TemperatureRegulatorWorkModeBuilderReplied(new BytesPair(_data[19], _data[20])).Build(),

                CalculatedTemperatureSetting = new BytesPair(_data[21], _data[22]).HighFirstUnsignedValue * 0.01,
                FanSpeed = new BytesPair(_data[23], _data[24]).HighFirstUnsignedValue,

                Diagnostic1 = new BytesPair(_data[25], _data[26]).HighFirstUnsignedValue,
                Diagnostic1Parsed = new MukFanVaporizerDataReply03Diagnostic1Builder(_data[26]).Build(),

                Diagnostic2 = new BytesPair(_data[27], _data[28]).HighFirstUnsignedValue,
                Diagnostic2Parsed = new MukFanVaporizerDataReply03Diagnostic2Builder(_data[28], _data[27]).Build(),

                Diagnostic3 = new BytesPair(_data[29], _data[30]).HighFirstUnsignedValue,
                Diagnostic3Parsed = new MukEmersionSwitchOnDiagnosticBuilder(new BytesPair(_data[29], _data[30]).HighFirstUnsignedValue).Build(),

                Diagnostic4 = new BytesPair(_data[31], _data[32]).HighFirstUnsignedValue,
                Diagnostic4OneWire1 = new MukFlapDiagnosticOneWireSensorBuilder(new BytesPair(_data[31], _data[32]).HighFirstUnsignedValue).Build(),

                Diagnostic5 = new BytesPair(_data[33], _data[34]).HighFirstUnsignedValue,
                Diagnostic5OneWire2 = new MukFlapDiagnosticOneWireSensorBuilder(new BytesPair(_data[33], _data[34]).HighFirstUnsignedValue).Build(),

                FirmwareBuildNumber = new BytesPair(_data[35], _data[36]).HighFirstUnsignedValue,
                TemperatureAddress3 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[37], _data[38]), 0.01, 0.0, NoSensor),
            };
        }
    }
}