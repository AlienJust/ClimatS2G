using System.Collections.Generic;
using System.Globalization;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	class SystemDiagnosticViewModel : ViewModelBase {
		private const string UnknownText = "Неизвестно";
		private const string NoLinkText = "Нет связи";
		private const string OkLinkText = "Есть связь";
		private const string NoSensorText = "Обрыв";

		private const Colors UnknownColor = Colors.Yellow;
		private const Colors NoLinkColor = Colors.Red;
		private const Colors NoSensorColor = Colors.Red;

		private const Colors OkLinkColor = Colors.LimeGreen;
		private const Colors OkSensorColor = Colors.LimeGreen;


		private const string OkDiagText = "Ok";
		private const string ErDiagText = "Er";
		private const Colors OkDiagColor = Colors.LimeGreen;
		private const Colors ErDiagColor = Colors.Red;

		private const Colors HiVoltageOnLine = Colors.Red;
		private const Colors HiVoltageOffLine = Colors.LimeGreen;

		private readonly IThreadNotifier _uiNotifier;
		private readonly ICmdListener<IMukFlapOuterAirReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukCondenserFanReply03;
		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Reply> _cmdListenerBsSmReply32;

		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;
		private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvs1Reply65;
		private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvs2Reply65;

		private string _segmentType;
		private bool _isSlave;
		private bool _isMaster;

		private string _version;
		private string _workStage;

		private string _mukInfo2;
		private Colors _mukInfoColor2;

		private string _mukInfo3;
		private Colors _mukInfoColor3;

		private string _mukInfo4;
		private Colors _mukInfoColor4;

		private string _mukInfo6;
		private Colors _mukInfoColor6;

		private string _mukInfo7;
		private Colors _mukInfoColor7;

		private string _mukInfo8;
		private Colors _mukInfoColor8;

		private string _bsSmInfo;
		private Colors _bsSmInfoColor;

		private string _bvsInfo1;
		private Colors _bvsInfoColor1;

		private string _bvsInfo2;
		private Colors _bvsInfoColor2;

		private string _emersonInfo;
		private Colors _emersonInfoColor;

		private string _evaporatorFanControllerInfo;
		private Colors _evaporatorFanControllerInfoColor;

		private string _concentratorInfo;
		private Colors _concentratorInfoColor;


		private Colors _voltage380Color;
		private Colors _voltage3000Color;

		private string _sensorOuterAirInfo;
		private Colors _sensorOuterAirInfoColor;

		private string _sensorRecycleAirInfo;
		private Colors _sensorRecycleAirInfoColor;

		/// <summary>
		/// Датчик подаваемого воздуха
		/// </summary>
		private string _sensorSupplyAirInfo;
		private Colors _sensorSupplyAirInfoColor;

		private string _sensorInteriorAirInfo1;
		private Colors _sensorInteriorAirInfoColor1;

		private string _sensorInteriorAirInfo2;
		private Colors _sensorInteriorAirInfoColor2;

		private string _emersonPressure1;
		private Colors _emersonPressure1Color;

		private string _emersonPressure2;
		private Colors _emersonPressure2Color;

		private string _emersonTemperature1;
		private Colors _emersonTemperature1Color;

		private string _emersonTemperature2;
		private Colors _emersonTemperature2Color;

		private string _condensorPressure1;
		private Colors _condensorPressure1Color;

		private string _condensorPressure2;
		private Colors _condensorPressure2Color;

		private string _flapAirOuterDiagInfo5;
		private Colors _flapAirOuterDiagInfo5Color;
		private string _flapAirOuterDiagInfo6;
		private Colors _flapAirOuterDiagInfo6Color;

		private string _flapAirRecycleDiagInfo5;
		private Colors _flapAirRecycleDiagInfo5Color;
		private string _flapAirRecycleDiagInfo6;
		private Colors _flapAirRecycleDiagInfo6Color;

		private string _flapAirWinterSummerDiagInfo5;
		private Colors _flapAirWinterSummerDiagInfo5Color;
		private string _flapAirWinterSummerDiagInfo6;
		private Colors _flapAirWinterSummerDiagInfo6Color;

		private Colors _fanEvaporatorColor;
		private string _fanAirExhausterInfo;

		private Colors _fanAirExhausterColor;
		private string _fanEvaporatorInfo;

		private Colors _fanCondensorColor;
		private string _fanCondensorInfo;

		private Colors _сoncentratorAdvancedColor;
		private string _concentratorAdvancedInfo;

		private string _calculatedTemperatureSetting;

		public AutoViewModel AutoVm1 { get; }
		public AutoViewModel AutoVm2 { get; }
		public AutoViewModel AutoVm3 { get; }
		public AutoViewModel AutoVm4 { get; }
		public AutoViewModel AutoVm5 { get; }
		public AutoViewModel AutoVm6 { get; }
		public AutoViewModel AutoVm7 { get; }
		public AutoViewModel AutoVm8 { get; }
		public AutoViewModel AutoVm9 { get; }

		public BsSmFaultViewModel BsSmFaultVm1 { get; }
		public BsSmFaultViewModel BsSmFaultVm2 { get; }
		public BsSmFaultViewModel BsSmFaultVm3 { get; }
		public BsSmFaultViewModel BsSmFaultVm4 { get; }
		public BsSmFaultViewModel BsSmFaultVm5 { get; }

		public bool IsFullVersion { get; }
		public bool IsHalfOrFullVersion { get; }

		public SystemDiagnosticViewModel(bool isFullVersion, bool isHalfOrFullVersion,
			IThreadNotifier uiNotifier,
			ICmdListener<IMukFlapOuterAirReply03Telemetry> cmdListenerMukFlapOuterAirReply03,
			ICmdListener<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
			ICmdListener<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16,
			ICmdListener<IMukCondensorFanReply03Data> cmdListenerMukCondenserFanReply03,
			ICmdListener<IMukAirExhausterReply03Data> cmdListenerMukAirExhausterReply03,
			ICmdListener<IMukFlapReturnAirReply03Telemetry> cmdListenerMukFlapReturnAirReply03,
			ICmdListener<IMukFlapWinterSummerReply03Telemetry> cmdListenerMukFlapWinterSummerReply03,
			ICmdListener<IBsSmAndKsm1DataCommand32Reply> cmdListenerBsSmReply32,
			ICmdListener<IList<BytesPair>> cmdListenerKsm,
			ICmdListener<IBvsReply65Telemetry> cmdListenerBvs1Reply65,
			ICmdListener<IBvsReply65Telemetry> cmdListenerBvs2Reply65) {

			IsFullVersion = isFullVersion;
			IsHalfOrFullVersion = isHalfOrFullVersion;

			_uiNotifier = uiNotifier;
			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerMukCondenserFanReply03 = cmdListenerMukCondenserFanReply03;
			_cmdListenerMukAirExhausterReply03 = cmdListenerMukAirExhausterReply03;
			_cmdListenerMukFlapReturnAirReply03 = cmdListenerMukFlapReturnAirReply03;
			_cmdListenerMukFlapWinterSummerReply03 = cmdListenerMukFlapWinterSummerReply03;
			_cmdListenerBsSmReply32 = cmdListenerBsSmReply32;
			_cmdListenerKsm = cmdListenerKsm;
			_cmdListenerBvs1Reply65 = cmdListenerBvs1Reply65;
			_cmdListenerBvs2Reply65 = cmdListenerBvs2Reply65;

			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
			_cmdListenerMukCondenserFanReply03.DataReceived += CmdListenerMukCondenserFanReply03OnDataReceived;
			_cmdListenerMukAirExhausterReply03.DataReceived += CmdListenerMukAirExhausterReply03OnDataReceived;
			_cmdListenerMukFlapReturnAirReply03.DataReceived += CmdListenerMukFlapReturnAirReply03OnDataReceived;
			_cmdListenerMukFlapWinterSummerReply03.DataReceived += CmdListenerMukFlapWinterSummerReply03OnDataReceived;
			_cmdListenerBsSmReply32.DataReceived += CmdListenerBsSmReply32OnDataReceived;
			_cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;

			_cmdListenerBvs1Reply65.DataReceived += CmdListenerBvs1Reply65OnDataReceived;
			_cmdListenerBvs2Reply65.DataReceived += CmdListenerBvs2Reply65OnDataReceived;

			ResetVmPropsToDefaultValues();
			AutoVm1 = new AutoViewModel("Автоматический выключатель приточного вентилятора 1");
			AutoVm2 = new AutoViewModel("Автоматический выключатель приточного вентилятора 2");
			AutoVm3 = new AutoViewModel("Автоматический выключатель вытяжных вентиляторов");
			AutoVm4 = new AutoViewModel("Автоматический выключатель Компрессора 1");
			AutoVm5 = new AutoViewModel("Автоматический выключатель Компрессора 2");
			AutoVm6 = new AutoViewModel("Автоматический выключатель нагревателя 380");
			AutoVm7 = new AutoViewModel("Автоматический выключатель рециркуляционных нагревателей");
			AutoVm8 = new AutoViewModel("Автоматический выключатель вентилятора конденсатора");
			AutoVm9 = new AutoViewModel("Автоматический выключатель обеззораживателя");

			BsSmFaultVm1 = new BsSmFaultViewModel();
			BsSmFaultVm2 = new BsSmFaultViewModel();
			BsSmFaultVm3 = new BsSmFaultViewModel();
			BsSmFaultVm4 = new BsSmFaultViewModel();
			BsSmFaultVm5 = new BsSmFaultViewModel();
		}



		/// <summary>
		/// МУК заслонки наружного воздуха, MODBUS адрес = 2
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapOuterAirReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo2 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor2 = OkLinkColor;

				if (data.Diagnostic1.NoEmersionControllerAnswer) {
					EmersonInfo = NoLinkText;
					EmersonInfoColor = NoLinkColor;

					EmersonPressure1 = NoLinkText;
					EmersonPressure1Color = NoLinkColor;
					EmersonPressure2 = NoLinkText;
					EmersonPressure2Color = NoLinkColor;
					EmersonTemperature1 = NoLinkText;
					EmersonTemperature1Color = NoLinkColor;
					EmersonTemperature2 = NoLinkText;
					EmersonTemperature2Color = NoLinkColor;
				}
				else {
					EmersonInfo = OkLinkText;
					EmersonInfoColor = OkLinkColor;

					if (data.EmersonPressureCircuit1.NoLinkWithSensor) {
						EmersonPressure1 = NoSensorText;
						EmersonPressure1Color = NoSensorColor;
					}
					else {
						EmersonPressure1 = data.EmersonPressureCircuit1.Indication.ToString("f2");
						EmersonPressure1Color = OkSensorColor;
					}

					if (data.EmersonPressureCircuit2.NoLinkWithSensor) {
						EmersonPressure2 = NoSensorText;
						EmersonPressure2Color = NoSensorColor;
					}
					else {
						EmersonPressure2 = data.EmersonPressureCircuit2.Indication.ToString("f2");
						EmersonPressure2Color = OkSensorColor;
					}

					if (data.EmersonTemperatureCircuit1.NoLinkWithSensor) {
						EmersonTemperature1 = NoSensorText;
						EmersonTemperature1Color = NoSensorColor;
					}
					else {
						EmersonTemperature1 = data.EmersonTemperatureCircuit1.Indication.ToString("f2");
						EmersonTemperature1Color = OkSensorColor;
					}

					if (data.EmersonTemperatureCircuit2.NoLinkWithSensor) {
						EmersonTemperature2 = NoSensorText;
						EmersonTemperature2Color = NoSensorColor;
					}
					else {
						EmersonTemperature2 = data.EmersonTemperatureCircuit2.Indication.ToString("f2");
						EmersonTemperature2Color = OkSensorColor;
					}
				}


				if (data.Diagnostic2.OsShowsFlapDoesNotReachLimitPositions) {
					FlapAirOuterDiagInfo5 = ErDiagText;
					FlapAirOuterDiagInfo5Color = ErDiagColor;
				}
				else {
					FlapAirOuterDiagInfo5 = OkDiagText;
					FlapAirOuterDiagInfo5Color = OkDiagColor;
				}

				if (data.Diagnostic2.OsShowsFlapDoesNotReach50Percent) {
					FlapAirOuterDiagInfo6 = ErDiagText;
					FlapAirOuterDiagInfo6Color = ErDiagColor;
				}
				else {
					FlapAirOuterDiagInfo6 = OkDiagText;
					FlapAirOuterDiagInfo6Color = OkDiagColor;
				}
			});
		}

		/// <summary>
		/// МУК вентилятора испарителя, MODBUS адрес = 3
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data) {
			_uiNotifier.Notify(() => {
				MukInfo3 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor3 = OkLinkColor;

				if (data.Diagnostic1Parsed.FanControllerLinkLost) {
					EvaporatorFanControllerInfo = NoLinkText;
					EvaporatorFanControllerInfoColor = NoLinkColor;
				}
				else {
					EvaporatorFanControllerInfo = OkLinkText;
					EvaporatorFanControllerInfoColor = OkLinkColor;
				}

				if (data.TemperatureAddress1.NoLinkWithSensor) {
					SensorOuterAirInfo = NoSensorText;
					SensorOuterAirInfoColor = NoSensorColor;
				}
				else {
					SensorOuterAirInfo = data.TemperatureAddress1.Indication.ToString("f2");
					SensorOuterAirInfoColor = OkSensorColor;
				}

				if (data.TemperatureAddress2.NoLinkWithSensor) {
					SensorRecycleAirInfo = NoSensorText;
					SensorRecycleAirInfoColor = NoSensorColor;
				}
				else {
					SensorRecycleAirInfo = data.TemperatureAddress2.Indication.ToString("f2");
					SensorRecycleAirInfoColor = OkSensorColor;
				}

				if (data.TemperatureAddress3.NoLinkWithSensor) {
					SensorSupplyAirInfo = NoSensorText;
					SensorSupplyAirInfoColor = NoSensorColor;
				}
				else {
					SensorSupplyAirInfo = data.TemperatureAddress3.Indication.ToString("f2");
					SensorSupplyAirInfoColor = OkSensorColor;
				}

				FanEvaporatorInfo = data.FanSpeed.ToString(CultureInfo.InvariantCulture);
				if (data.Diagnostic1.GetBit(4)) {
					FanEvaporatorColor = ErDiagColor;
					FanEvaporatorInfo += ", неисправность";
				}
				else {
					FanEvaporatorColor = OkDiagColor;
					FanEvaporatorInfo += ", норма";
				}

				CalculatedTemperatureSetting = data.CalculatedTemperatureSetting.ToString("f2");
			});
		}

		/// <summary>
		/// МУК вентилятора конденсатора, MODBUS адрес = 4
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukCondenserFanReply03OnDataReceived(IList<byte> bytes, IMukCondensorFanReply03Data data) {
			_uiNotifier.Notify(() => {
				MukInfo4 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor4 = OkLinkColor;

				if (data.CondensingPressure.NoLinkWithSensor) {
					CondensorPressure1 = NoSensorText;
					CondensorPressure1Color = NoSensorColor;
				}
				else {
					CondensorPressure1 = data.CondensingPressure.Indication.ToString("f2");
					CondensorPressure1Color = OkSensorColor;
				}

				if (data.CondensingPressure2.NoLinkWithSensor) {
					CondensorPressure2 = NoSensorText;
					CondensorPressure2Color = NoSensorColor;
				}
				else {
					CondensorPressure2 = data.CondensingPressure2.Indication.ToString("f2");
					CondensorPressure2Color = OkSensorColor;
				}

				FanCondensorInfo = string.Empty;
				if (data.Stage1IsOn)
					FanCondensorInfo += "Ступень 1   ";
				if (data.Stage1IsOn)
					FanCondensorInfo += "Ступень 2   ";
				if (data.Diagnostic1.GetBit(5)) {
					FanCondensorInfo += "Неисправность";
					FanCondensorColor = ErDiagColor;
				}
				else {
					FanCondensorInfo += "Норма";
					FanCondensorColor = OkDiagColor;
				}
			});
		}

		/// <summary>
		/// МУК вытяжного вентилятора, MODBUS адрес = 6
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukAirExhausterReply03OnDataReceived(IList<byte> bytes, IMukAirExhausterReply03Data data) {
			_uiNotifier.Notify(() => {
				MukInfo6 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor6 = OkLinkColor;

				FanAirExhausterInfo = data.HeatPwm.ToString(CultureInfo.InvariantCulture);
				if (data.Diagnostic1.GetBit(5)) {
					FanAirExhausterColor = ErDiagColor;
					FanAirExhausterInfo += ", неисправность";
				}
				else {
					FanAirExhausterColor = OkDiagColor;
					FanAirExhausterInfo += ", норма";
				}
			});
		}

		/// <summary>
		/// МУК заслонки рециркуляционного воздуха, MODBUS адрес = 7
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukFlapReturnAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReturnAirReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo7 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor7 = OkLinkColor;

				if (data.Diagnostic2.OsShowsFlapDoesNotReachLimitPositions) {
					FlapAirRecycleDiagInfo5 = ErDiagText;
					FlapAirRecycleDiagInfo5Color = ErDiagColor;
				}
				else {
					FlapAirRecycleDiagInfo5 = OkDiagText;
					FlapAirRecycleDiagInfo5Color = OkDiagColor;
				}

				if (data.Diagnostic2.OsShowsFlapDoesNotReach50Percent) {
					FlapAirRecycleDiagInfo6 = ErDiagText;
					FlapAirRecycleDiagInfo6Color = ErDiagColor;
				}
				else {
					FlapAirRecycleDiagInfo6 = OkDiagText;
					FlapAirRecycleDiagInfo6Color = OkDiagColor;
				}

				if (data.Diagnostic1.HighVoltageKeyDriverLinkError) {
					ConcentratorInfo = NoLinkText;
					ConcentratorInfoColor = NoLinkColor;
				}
				else {
					ConcentratorInfo = OkLinkText;
					ConcentratorInfoColor = OkLinkColor;
				}

				if (data.ConcentratorStatusParsed.WorkOrError || data.ConcentratorStatusParsed.ErrorNoAnswerFromDriver
						|| data.ConcentratorStatusParsed.ErrorByCurrentCc) {
					ConcentratorAdvancedInfo = "Неисправность";
					ConcentratorAdvancedColor = ErDiagColor;
				}
				else {
					ConcentratorAdvancedInfo = "Норма";
					ConcentratorAdvancedColor = OkDiagColor;
				}
				ConcentratorAdvancedInfo += ", " + data.ConcentratorVoltage + "В";
			});
		}

		/// <summary>
		/// МУК заслонки зима-лето, MODBUS адрес = 8
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="data"></param>
		private void CmdListenerMukFlapWinterSummerReply03OnDataReceived(IList<byte> bytes, IMukFlapWinterSummerReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo8 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
				MukInfoColor8 = OkLinkColor;

				if (data.Diagnostic2.OsShowsFlapDoesNotReachLimitPositions) {
					FlapAirWinterSummerDiagInfo5 = ErDiagText;
					FlapAirWinterSummerDiagInfo5Color = ErDiagColor;
				}
				else {
					FlapAirWinterSummerDiagInfo5 = OkDiagText;
					FlapAirWinterSummerDiagInfo5Color = OkDiagColor;
				}

				if (data.Diagnostic2.OsShowsFlapDoesNotReach50Percent) {
					FlapAirWinterSummerDiagInfo6 = ErDiagText;
					FlapAirWinterSummerDiagInfo6Color = ErDiagColor;
				}
				else {
					FlapAirWinterSummerDiagInfo6 = OkDiagText;
					FlapAirWinterSummerDiagInfo6Color = OkDiagColor;
				}
			});
		}

		private void CmdListenerBsSmReply32OnDataReceived(IList<byte> bytes, IBsSmAndKsm1DataCommand32Reply data) {
			_uiNotifier.Notify(() => {
				BsSmInfo = IsHalfOrFullVersion ? new TextFormatterIntegerDotted().Format(data.BsSmVersionNumber) : OkLinkText;
				//BsSmInfo = new TextFormatterIntegerDotted().Format(data.BsSmVersionNumber);
				BsSmInfoColor = OkLinkColor;
				BsSmFaultVm1.Code = data.Ksm2Request.Fault1;
				BsSmFaultVm2.Code = data.Ksm2Request.Fault2;
				BsSmFaultVm3.Code = data.Ksm2Request.Fault3;
				BsSmFaultVm4.Code = data.Ksm2Request.Fault4;
				BsSmFaultVm5.Code = data.Ksm2Request.Fault5;

				Voltage3000Color = data.WorkModeAndCompressorSwitch.HasVoltage3000V ? HiVoltageOnLine : HiVoltageOffLine;
			});
		}


		private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data) {
			_uiNotifier.Notify(() => {
				Version = IsFullVersion ? new TextFormatterDotted(UnknownText).Format(data[34]) : OkLinkText;
				WorkStage = new TextFormatterWorkStage().Format(data[8]);

				// КСМ, бит "Нет связи с МУК заслонки наружного воздуха" взведен
				if (data[22].HighFirstUnsignedValue.GetBit(0)) {
					MukInfo2 = NoLinkText;
					MukInfoColor2 = NoLinkColor;

					FlapAirOuterDiagInfo5Color = NoLinkColor;
					FlapAirOuterDiagInfo5 = NoLinkText;

					FlapAirOuterDiagInfo6Color = NoLinkColor;
					FlapAirOuterDiagInfo6 = NoLinkText;

					// TODO: do I need check emerson here?
					EmersonInfo = NoLinkText;
					EmersonInfoColor = NoLinkColor;
				}

				// КСМ, бит "Нет связи с МУК вентилятора испарителя" взведен
				if (data[22].HighFirstUnsignedValue.GetBit(2)) {
					MukInfo3 = NoLinkText;
					MukInfoColor3 = NoLinkColor;

					EvaporatorFanControllerInfo = NoLinkText;
					EvaporatorFanControllerInfoColor = NoLinkColor;

					SensorOuterAirInfo = NoLinkText;
					SensorOuterAirInfoColor = NoLinkColor;

					SensorRecycleAirInfo = NoLinkText;
					SensorRecycleAirInfoColor = NoLinkColor;

					SensorSupplyAirInfo = NoLinkText;
					SensorSupplyAirInfoColor = NoLinkColor;

					FanEvaporatorInfo = NoLinkText;

					IsMaster = true;
					IsSlave = true;
					SegmentType = UnknownText;

					CalculatedTemperatureSetting = NoLinkText;
				}

				if (data[22].HighFirstUnsignedValue.GetBit(4)) {
					MukInfo4 = NoLinkText;
					MukInfoColor4 = NoLinkColor;
				}

				// КСМ, бит "Нет связи с МУК вытяжного вентилятора" взведен
				if (data[22].HighFirstUnsignedValue.GetBit(6)) {
					MukInfo6 = NoLinkText;
					MukInfoColor6 = NoLinkColor;

					FanAirExhausterInfo = NoLinkText;
				}

				// КСМ, бит "Нет связи с МУК заслонки рециркуляционого воздуха" взведен
				if (data[23].HighFirstUnsignedValue.GetBit(0)) {
					MukInfo7 = NoLinkText;
					MukInfoColor7 = NoLinkColor;

					FlapAirRecycleDiagInfo5Color = NoLinkColor;
					FlapAirRecycleDiagInfo5 = NoLinkText;

					FlapAirRecycleDiagInfo6Color = NoLinkColor;
					FlapAirRecycleDiagInfo6 = NoLinkText;

					ConcentratorInfoColor = NoLinkColor;
					ConcentratorInfo = NoLinkText;
				}

				// КСМ, бит "Нет связи с МУК заслонки лето-зима" взведен
				if (data[23].HighFirstUnsignedValue.GetBit(2)) {
					MukInfo8 = NoLinkText;
					MukInfoColor8 = NoLinkColor;

					FlapAirWinterSummerDiagInfo5Color = NoLinkColor;
					FlapAirWinterSummerDiagInfo5 = NoLinkText;

					FlapAirWinterSummerDiagInfo6Color = NoLinkColor;
					FlapAirWinterSummerDiagInfo6 = NoLinkText;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(4)) {
					BsSmInfo = NoLinkText;
					BsSmInfoColor = NoLinkColor;
					Voltage3000Color = UnknownColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(5)) {
					BvsInfo1 = NoLinkText;
					BvsInfoColor1 = NoLinkColor;
					Voltage380Color = UnknownColor;
				}
				else {
					BvsInfo1 = OkLinkText;
					BvsInfoColor1 = OkLinkColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(6)) {
					BvsInfo2 = NoLinkText;
					BvsInfoColor2 = NoLinkColor;
				}
				else {
					BvsInfo2 = OkLinkText;
					BvsInfoColor2 = OkLinkColor;
				}

				var oneWireSensor1 = new SensorIndicationDoubleBasedOnBytesPair(data[0], 0.01, 0.0, new BytesPair(0x85, 0x00));
				if (oneWireSensor1.NoLinkWithSensor) {
					SensorInteriorAirInfo1 = NoSensorText;
					SensorInteriorAirInfoColor1 = NoSensorColor;
				}
				else {
					SensorInteriorAirInfo1 = oneWireSensor1.Indication.ToString("f2");
					SensorInteriorAirInfoColor1 = OkSensorColor;
				}

				var oneWireSensor2 = new SensorIndicationDoubleBasedOnBytesPair(data[1], 0.01, 0.0, new BytesPair(0x85, 0x00));
				if (oneWireSensor2.NoLinkWithSensor) {
					SensorInteriorAirInfo2 = NoSensorText;
					SensorInteriorAirInfoColor2 = NoSensorColor;
				}
				else {
					SensorInteriorAirInfo2 = oneWireSensor2.Indication.ToString("f2");
					SensorInteriorAirInfoColor2 = OkSensorColor;
				}
			});
		}

		private void CmdListenerMukVaporizerRequest16DataReceived(IList<byte> bytes, IMukFanVaporizerDataRequest16 data) {
			_uiNotifier.Notify(() => {
				if (data != null) {
					if (data.IsSlave) {
						SegmentType = "Slave";
						IsSlave = true;
						IsMaster = false;
					}
					else {
						SegmentType = "Master";
						IsSlave = false;
						IsMaster = true;
					}
				}
			});
		}

		private void CmdListenerBvs1Reply65OnDataReceived(IList<byte> bytes, IBvsReply65Telemetry data) {
			_uiNotifier.Notify(() => {
				AutoVm1.IsOk = data.BvsInput13; // 2.4
				AutoVm4.IsOk = data.BvsInput9; // 2.0
				AutoVm6.IsOk = data.BvsInput11; // 2.2
				AutoVm7.IsOk = data.BvsInput12; // 2.3
				AutoVm8.IsOk = data.BvsInput10; // 2.1

				Voltage380Color = data.BvsInput1 ? HiVoltageOnLine : HiVoltageOffLine;
			});

		}

		private void CmdListenerBvs2Reply65OnDataReceived(IList<byte> bytes, IBvsReply65Telemetry data) {
			_uiNotifier.Notify(() => {
				AutoVm2.IsOk = data.BvsInput13; // 2.4
				AutoVm3.IsOk = data.BvsInput10; // 2.1
				AutoVm5.IsOk = data.BvsInput9; // 2.0
				AutoVm9.IsOk = data.BvsInput7; // 1.6
			});
		}

		public string SegmentType {
			get => _segmentType;
			set {
				if (_segmentType != value) {
					_segmentType = value;
					RaisePropertyChanged(() => SegmentType);
				}
			}
		}

		public bool IsSlave {
			get => _isSlave;
			set {
				if (_isSlave != value) {
					_isSlave = value;
					RaisePropertyChanged(() => IsSlave);
				}
			}
		}
		public bool IsMaster {
			get => _isMaster;
			set {
				if (_isMaster != value) {
					_isMaster = value;
					RaisePropertyChanged(() => IsMaster);
				}
			}
		}

		public string Version {
			get => _version;
			set {
				if (_version != value) {
					_version = value;
					RaisePropertyChanged(() => Version);
				}
			}
		}

		public string WorkStage {
			get => _workStage;
			set {
				if (_workStage != value) {
					_workStage = value;
					RaisePropertyChanged(() => WorkStage);
				}
			}
		}


		public string MukInfo2 {
			get => _mukInfo2;
			set {
				if (_mukInfo2 != value) {
					_mukInfo2 = value;
					RaisePropertyChanged(() => MukInfo2);
				}
			}
		}
		public Colors MukInfoColor2 {
			get => _mukInfoColor2;
			set {
				if (_mukInfoColor2 != value) {
					_mukInfoColor2 = value;
					RaisePropertyChanged(() => MukInfoColor2);
				}
			}
		}


		public string MukInfo3 {
			get => _mukInfo3;
			set {
				if (_mukInfo3 != value) {
					_mukInfo3 = value;
					RaisePropertyChanged(() => MukInfo3);
				}
			}
		}
		public Colors MukInfoColor3 {
			get => _mukInfoColor3;
			set {
				if (_mukInfoColor3 != value) {
					_mukInfoColor3 = value;
					RaisePropertyChanged(() => MukInfoColor3);
				}
			}
		}


		public string MukInfo4 {
			get => _mukInfo4;
			set {
				if (_mukInfo4 != value) {
					_mukInfo4 = value;
					RaisePropertyChanged(() => MukInfo4);
				}
			}
		}
		public Colors MukInfoColor4 {
			get => _mukInfoColor4;
			set {
				if (_mukInfoColor4 != value) {
					_mukInfoColor4 = value;
					RaisePropertyChanged(() => MukInfoColor4);
				}
			}
		}


		public string MukInfo6 {
			get => _mukInfo6;
			set {
				if (_mukInfo6 != value) {
					_mukInfo6 = value;
					RaisePropertyChanged(() => MukInfo6);
				}
			}
		}
		public Colors MukInfoColor6 {
			get => _mukInfoColor6;
			set {
				if (_mukInfoColor6 != value) {
					_mukInfoColor6 = value;
					RaisePropertyChanged(() => MukInfoColor6);
				}
			}
		}


		public string MukInfo7 {
			get => _mukInfo7;
			set {
				if (_mukInfo7 != value) {
					_mukInfo7 = value;
					RaisePropertyChanged(() => MukInfo7);
				}
			}
		}
		public Colors MukInfoColor7 {
			get => _mukInfoColor7;
			set {
				if (_mukInfoColor7 != value) {
					_mukInfoColor7 = value;
					RaisePropertyChanged(() => MukInfoColor7);
				}
			}
		}


		public string MukInfo8 {
			get => _mukInfo8;
			set {
				if (_mukInfo8 != value) {
					_mukInfo8 = value;
					RaisePropertyChanged(() => MukInfo8);
				}
			}
		}
		public Colors MukInfoColor8 {
			get => _mukInfoColor8;
			set {
				if (_mukInfoColor8 != value) {
					_mukInfoColor8 = value;
					RaisePropertyChanged(() => MukInfoColor8);
				}
			}
		}


		public string BsSmInfo {
			get => _bsSmInfo;
			set {
				if (_bsSmInfo != value) {
					_bsSmInfo = value;
					RaisePropertyChanged(() => BsSmInfo);
				}
			}
		}
		public Colors BsSmInfoColor {
			get => _bsSmInfoColor;
			set {
				if (_bsSmInfoColor != value) {
					_bsSmInfoColor = value;
					RaisePropertyChanged(() => BsSmInfoColor);
				}
			}
		}


		public string BvsInfo1 {
			get => _bvsInfo1;
			set {
				if (_bvsInfo1 != value) {
					_bvsInfo1 = value;
					RaisePropertyChanged(() => BvsInfo1);
				}
			}
		}
		public Colors BvsInfoColor1 {
			get => _bvsInfoColor1;
			set {
				if (_bvsInfoColor1 != value) {
					_bvsInfoColor1 = value;
					RaisePropertyChanged(() => BvsInfoColor1);
				}
			}
		}


		public string BvsInfo2 {
			get => _bvsInfo2;
			set {
				if (_bvsInfo2 != value) {
					_bvsInfo2 = value;
					RaisePropertyChanged(() => BvsInfo2);
				}
			}
		}
		public Colors BvsInfoColor2 {
			get => _bvsInfoColor2;
			set {
				if (_bvsInfoColor2 != value) {
					_bvsInfoColor2 = value;
					RaisePropertyChanged(() => BvsInfoColor2);
				}
			}
		}


		public string EmersonInfo {
			get => _emersonInfo;
			set {
				if (_emersonInfo != value) {
					_emersonInfo = value;
					RaisePropertyChanged(() => EmersonInfo);
				}
			}
		}
		public Colors EmersonInfoColor {
			get => _emersonInfoColor;
			set {
				if (_emersonInfoColor != value) {
					_emersonInfoColor = value;
					RaisePropertyChanged(() => EmersonInfoColor);
				}
			}
		}



		public string EvaporatorFanControllerInfo {
			get => _evaporatorFanControllerInfo;
			set {
				if (_evaporatorFanControllerInfo != value) {
					_evaporatorFanControllerInfo = value;
					RaisePropertyChanged(() => EvaporatorFanControllerInfo);
				}
			}
		}
		public Colors EvaporatorFanControllerInfoColor {
			get => _evaporatorFanControllerInfoColor;
			set {
				if (_evaporatorFanControllerInfoColor != value) {
					_evaporatorFanControllerInfoColor = value;
					RaisePropertyChanged(() => EvaporatorFanControllerInfoColor);
				}
			}
		}

		public string ConcentratorInfo {
			get => _concentratorInfo;
			set {
				if (_concentratorInfo != value) {
					_concentratorInfo = value;
					RaisePropertyChanged(() => ConcentratorInfo);
				}
			}
		}
		public Colors ConcentratorInfoColor {
			get => _concentratorInfoColor;
			set {
				if (_concentratorInfoColor != value) {
					_concentratorInfoColor = value;
					RaisePropertyChanged(() => ConcentratorInfoColor);
				}
			}
		}

		public Colors Voltage380Color {
			get => _voltage380Color;
			set {
				if (_voltage380Color != value) {
					_voltage380Color = value;
					RaisePropertyChanged(() => Voltage380Color);
				}
			}
		}
		public Colors Voltage3000Color {
			get => _voltage3000Color;
			set {
				if (_voltage3000Color != value) {
					_voltage3000Color = value;
					RaisePropertyChanged(() => Voltage3000Color);
				}
			}
		}

		public string SensorOuterAirInfo {
			get => _sensorOuterAirInfo;
			set {
				if (_sensorOuterAirInfo != value) {
					_sensorOuterAirInfo = value;
					RaisePropertyChanged(() => SensorOuterAirInfo);
				}
			}
		}
		public Colors SensorOuterAirInfoColor {
			get => _sensorOuterAirInfoColor;
			set {
				if (_sensorOuterAirInfoColor != value) {
					_sensorOuterAirInfoColor = value;
					RaisePropertyChanged(() => SensorOuterAirInfoColor);
				}
			}
		}


		public string SensorRecycleAirInfo {
			get => _sensorRecycleAirInfo;
			set {
				if (_sensorRecycleAirInfo != value) {
					_sensorRecycleAirInfo = value;
					RaisePropertyChanged(() => SensorRecycleAirInfo);
				}
			}
		}
		public Colors SensorRecycleAirInfoColor {
			get => _sensorRecycleAirInfoColor;
			set {
				if (_sensorRecycleAirInfoColor != value) {
					_sensorRecycleAirInfoColor = value;
					RaisePropertyChanged(() => SensorRecycleAirInfoColor);
				}
			}
		}

		#region Props Датчик подаваемого воздуха
		public string SensorSupplyAirInfo {
			get => _sensorSupplyAirInfo;
			set {
				if (_sensorSupplyAirInfo != value) {
					_sensorSupplyAirInfo = value;
					RaisePropertyChanged(() => SensorSupplyAirInfo);
				}
			}
		}
		public Colors SensorSupplyAirInfoColor {
			get => _sensorSupplyAirInfoColor;
			set {
				if (_sensorSupplyAirInfoColor != value) {
					_sensorSupplyAirInfoColor = value;
					RaisePropertyChanged(() => SensorSupplyAirInfoColor);
				}
			}
		}
		#endregion

		public string SensorInteriorAirInfo1 {
			get => _sensorInteriorAirInfo1;
			set {
				if (_sensorInteriorAirInfo1 != value) {
					_sensorInteriorAirInfo1 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfo1);
				}
			}
		}
		public Colors SensorInteriorAirInfoColor1 {
			get => _sensorInteriorAirInfoColor1;
			set {
				if (_sensorInteriorAirInfoColor1 != value) {
					_sensorInteriorAirInfoColor1 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfoColor1);
				}
			}
		}


		public string SensorInteriorAirInfo2 {
			get => _sensorInteriorAirInfo2;
			set {
				if (_sensorInteriorAirInfo2 != value) {
					_sensorInteriorAirInfo2 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfo2);
				}
			}
		}
		public Colors SensorInteriorAirInfoColor2 {
			get => _sensorInteriorAirInfoColor2;
			set {
				if (_sensorInteriorAirInfoColor2 != value) {
					_sensorInteriorAirInfoColor2 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfoColor2);
				}
			}
		}


		public string EmersonPressure1 {
			get => _emersonPressure1;
			set {
				if (_emersonPressure1 != value) {
					_emersonPressure1 = value;
					RaisePropertyChanged(() => EmersonPressure1);
				}
			}
		}
		public Colors EmersonPressure1Color {
			get => _emersonPressure1Color;
			set {
				if (_emersonPressure1Color != value) {
					_emersonPressure1Color = value;
					RaisePropertyChanged(() => EmersonPressure1Color);
				}
			}
		}


		public string EmersonPressure2 {
			get => _emersonPressure2;
			set {
				if (_emersonPressure2 != value) {
					_emersonPressure2 = value;
					RaisePropertyChanged(() => EmersonPressure2);
				}
			}
		}
		public Colors EmersonPressure2Color {
			get => _emersonPressure2Color;
			set {
				if (_emersonPressure2Color != value) {
					_emersonPressure2Color = value;
					RaisePropertyChanged(() => EmersonPressure2Color);
				}
			}
		}


		public string EmersonTemperature1 {
			get => _emersonTemperature1;
			set {
				if (_emersonTemperature1 != value) {
					_emersonTemperature1 = value;
					RaisePropertyChanged(() => EmersonTemperature1);
				}
			}
		}
		public Colors EmersonTemperature1Color {
			get => _emersonTemperature1Color;
			set {
				if (_emersonTemperature1Color != value) {
					_emersonTemperature1Color = value;
					RaisePropertyChanged(() => EmersonTemperature1Color);
				}
			}
		}

		public string EmersonTemperature2 {
			get => _emersonTemperature2;
			set {
				if (_emersonTemperature2 != value) {
					_emersonTemperature2 = value;
					RaisePropertyChanged(() => EmersonTemperature2);
				}
			}
		}
		public Colors EmersonTemperature2Color {
			get => _emersonTemperature2Color;
			set {
				if (_emersonTemperature2Color != value) {
					_emersonTemperature2Color = value;
					RaisePropertyChanged(() => EmersonTemperature2Color);
				}
			}
		}



		public string CondensorPressure1 {
			get => _condensorPressure1;
			set {
				if (_condensorPressure1 != value) {
					_condensorPressure1 = value;
					RaisePropertyChanged(() => CondensorPressure1);
				}
			}
		}
		public Colors CondensorPressure1Color {
			get => _condensorPressure1Color;
			set {
				if (_condensorPressure1Color != value) {
					_condensorPressure1Color = value;
					RaisePropertyChanged(() => CondensorPressure1Color);
				}
			}
		}


		public string CondensorPressure2 {
			get => _condensorPressure2;
			set {
				if (_condensorPressure2 != value) {
					_condensorPressure2 = value;
					RaisePropertyChanged(() => CondensorPressure2);
				}
			}
		}
		public Colors CondensorPressure2Color {
			get => _condensorPressure2Color;
			set {
				if (_condensorPressure2Color != value) {
					_condensorPressure2Color = value;
					RaisePropertyChanged(() => CondensorPressure2Color);
				}
			}
		}


		public string FlapAirOuterDiagInfo5 {
			get => _flapAirOuterDiagInfo5;
			set {
				if (_flapAirOuterDiagInfo5 != value) {
					_flapAirOuterDiagInfo5 = value;
					RaisePropertyChanged(() => FlapAirOuterDiagInfo5);
				}
			}
		}
		public Colors FlapAirOuterDiagInfo5Color {
			get => _flapAirOuterDiagInfo5Color;
			set {
				if (_flapAirOuterDiagInfo5Color != value) {
					_flapAirOuterDiagInfo5Color = value;
					RaisePropertyChanged(() => FlapAirOuterDiagInfo5Color);
				}
			}
		}
		public string FlapAirOuterDiagInfo6 {
			get => _flapAirOuterDiagInfo6;
			set {
				if (_flapAirOuterDiagInfo6 != value) {
					_flapAirOuterDiagInfo6 = value;
					RaisePropertyChanged(() => FlapAirOuterDiagInfo6);
				}
			}
		}
		public Colors FlapAirOuterDiagInfo6Color {
			get => _flapAirOuterDiagInfo6Color;
			set {
				if (_flapAirOuterDiagInfo6Color != value) {
					_flapAirOuterDiagInfo6Color = value;
					RaisePropertyChanged(() => FlapAirOuterDiagInfo6Color);
				}
			}
		}


		public string FlapAirRecycleDiagInfo5 {
			get => _flapAirRecycleDiagInfo5;
			set {
				if (_flapAirRecycleDiagInfo5 != value) {
					_flapAirRecycleDiagInfo5 = value;
					RaisePropertyChanged(() => FlapAirRecycleDiagInfo5);
				}
			}
		}
		public Colors FlapAirRecycleDiagInfo5Color {
			get => _flapAirRecycleDiagInfo5Color;
			set {
				if (_flapAirRecycleDiagInfo5Color != value) {
					_flapAirRecycleDiagInfo5Color = value;
					RaisePropertyChanged(() => FlapAirRecycleDiagInfo5Color);
				}
			}
		}
		public string FlapAirRecycleDiagInfo6 {
			get => _flapAirRecycleDiagInfo6;
			set {
				if (_flapAirRecycleDiagInfo6 != value) {
					_flapAirRecycleDiagInfo6 = value;
					RaisePropertyChanged(() => FlapAirRecycleDiagInfo6);
				}
			}
		}
		public Colors FlapAirRecycleDiagInfo6Color {
			get => _flapAirRecycleDiagInfo6Color;
			set {
				if (_flapAirRecycleDiagInfo6Color != value) {
					_flapAirRecycleDiagInfo6Color = value;
					RaisePropertyChanged(() => FlapAirRecycleDiagInfo6Color);
				}
			}
		}


		public string FlapAirWinterSummerDiagInfo5 {
			get => _flapAirWinterSummerDiagInfo5;
			set {
				if (_flapAirWinterSummerDiagInfo5 != value) {
					_flapAirWinterSummerDiagInfo5 = value;
					RaisePropertyChanged(() => FlapAirWinterSummerDiagInfo5);
				}
			}
		}
		public Colors FlapAirWinterSummerDiagInfo5Color {
			get => _flapAirWinterSummerDiagInfo5Color;
			set {
				if (_flapAirWinterSummerDiagInfo5Color != value) {
					_flapAirWinterSummerDiagInfo5Color = value;
					RaisePropertyChanged(() => FlapAirWinterSummerDiagInfo5Color);
				}
			}
		}
		public string FlapAirWinterSummerDiagInfo6 {
			get => _flapAirWinterSummerDiagInfo6;
			set {
				if (_flapAirWinterSummerDiagInfo6 != value) {
					_flapAirWinterSummerDiagInfo6 = value;
					RaisePropertyChanged(() => FlapAirWinterSummerDiagInfo6);
				}
			}
		}
		public Colors FlapAirWinterSummerDiagInfo6Color {
			get => _flapAirWinterSummerDiagInfo6Color;
			set {
				if (_flapAirWinterSummerDiagInfo6Color != value) {
					_flapAirWinterSummerDiagInfo6Color = value;
					RaisePropertyChanged(() => FlapAirWinterSummerDiagInfo6Color);
				}
			}
		}

		public Colors FanAirExhausterColor {
			get => _fanAirExhausterColor;
			set {
				if (_fanAirExhausterColor != value) {
					_fanAirExhausterColor = value;
					RaisePropertyChanged(() => FanAirExhausterColor);
				}
			}
		}
		public string FanAirExhausterInfo {
			get => _fanAirExhausterInfo;
			set {
				if (_fanAirExhausterInfo != value) {
					_fanAirExhausterInfo = value;
					RaisePropertyChanged(() => FanAirExhausterInfo);
				}
			}
		}

		public Colors FanEvaporatorColor {
			get => _fanEvaporatorColor;
			set {
				if (_fanEvaporatorColor != value) {
					_fanEvaporatorColor = value;
					RaisePropertyChanged(() => FanEvaporatorColor);
				}
			}
		}
		public string FanEvaporatorInfo {
			get => _fanEvaporatorInfo;
			set {
				if (_fanEvaporatorInfo != value) {
					_fanEvaporatorInfo = value;
					RaisePropertyChanged(() => FanEvaporatorInfo);
				}
			}
		}

		public Colors FanCondensorColor {
			get => _fanCondensorColor;
			set {
				if (_fanCondensorColor != value) {
					_fanCondensorColor = value;
					RaisePropertyChanged(() => FanCondensorColor);
				}
			}
		}
		public string FanCondensorInfo {
			get => _fanCondensorInfo;
			set {
				if (_fanCondensorInfo != value) {
					_fanCondensorInfo = value;
					RaisePropertyChanged(() => FanCondensorInfo);
				}
			}
		}

		public Colors ConcentratorAdvancedColor {
			get => _сoncentratorAdvancedColor;
			set {
				if (_сoncentratorAdvancedColor != value) {
					_сoncentratorAdvancedColor = value;
					RaisePropertyChanged(() => ConcentratorAdvancedColor);
				}
			}
		}
		public string ConcentratorAdvancedInfo {
			get => _concentratorAdvancedInfo;
			set {
				if (_concentratorAdvancedInfo != value) {
					_concentratorAdvancedInfo = value;
					RaisePropertyChanged(() => ConcentratorAdvancedInfo);
				}
			}
		}

		public string CalculatedTemperatureSetting {
			get => _calculatedTemperatureSetting;
			set {
				if (_calculatedTemperatureSetting != value) {
					_calculatedTemperatureSetting = value;
					RaisePropertyChanged(() => CalculatedTemperatureSetting);
				}
			}
		}

		void ResetVmPropsToDefaultValues() {
			SegmentType = UnknownText;
			Version = UnknownText;
			WorkStage = UnknownText;

			MukInfo2 = UnknownText;
			MukInfoColor2 = UnknownColor;

			MukInfo3 = UnknownText;
			MukInfoColor3 = UnknownColor;

			MukInfo4 = UnknownText;
			MukInfoColor4 = UnknownColor;

			MukInfo6 = UnknownText;
			MukInfoColor6 = UnknownColor;

			MukInfo7 = UnknownText;
			MukInfoColor7 = UnknownColor;

			MukInfo8 = UnknownText;
			MukInfoColor8 = UnknownColor;

			BsSmInfo = UnknownText;
			BsSmInfoColor = UnknownColor;

			BvsInfo1 = UnknownText;
			BvsInfoColor1 = UnknownColor;

			BvsInfo2 = UnknownText;
			BvsInfoColor2 = UnknownColor;

			EmersonInfo = UnknownText;
			EmersonInfoColor = UnknownColor;

			EvaporatorFanControllerInfo = UnknownText;
			EvaporatorFanControllerInfoColor = UnknownColor;

			ConcentratorInfo = UnknownText;
			ConcentratorInfoColor = UnknownColor;

			Voltage380Color = UnknownColor;
			Voltage3000Color = UnknownColor;

			SensorOuterAirInfo = UnknownText;
			SensorOuterAirInfoColor = UnknownColor;

			SensorRecycleAirInfo = UnknownText;
			SensorRecycleAirInfoColor = UnknownColor;

			SensorSupplyAirInfo = UnknownText;
			SensorSupplyAirInfoColor = UnknownColor;

			SensorInteriorAirInfo1 = UnknownText;
			SensorInteriorAirInfoColor1 = UnknownColor;

			SensorInteriorAirInfo2 = UnknownText;
			SensorInteriorAirInfoColor2 = UnknownColor;

			EmersonPressure1 = UnknownText;
			EmersonPressure1Color = UnknownColor;
			EmersonPressure2 = UnknownText;
			EmersonPressure2Color = UnknownColor;
			EmersonTemperature1 = UnknownText;
			EmersonTemperature1Color = UnknownColor;
			EmersonTemperature2 = UnknownText;
			EmersonTemperature2Color = UnknownColor;

			CondensorPressure1 = UnknownText;
			CondensorPressure1Color = UnknownColor;
			CondensorPressure2 = UnknownText;
			CondensorPressure2Color = UnknownColor;

			FlapAirOuterDiagInfo5 = UnknownText;
			FlapAirOuterDiagInfo5Color = UnknownColor;
			FlapAirOuterDiagInfo6 = UnknownText;
			FlapAirOuterDiagInfo6Color = UnknownColor;

			FlapAirRecycleDiagInfo5 = UnknownText;
			FlapAirRecycleDiagInfo5Color = UnknownColor;
			FlapAirRecycleDiagInfo6 = UnknownText;
			FlapAirRecycleDiagInfo6Color = UnknownColor;

			FlapAirWinterSummerDiagInfo5 = UnknownText;
			FlapAirWinterSummerDiagInfo5Color = UnknownColor;
			FlapAirWinterSummerDiagInfo6 = UnknownText;
			FlapAirWinterSummerDiagInfo6Color = UnknownColor;

			FanAirExhausterInfo = UnknownText;
			FanAirExhausterColor = UnknownColor;

			FanEvaporatorInfo = UnknownText;
			FanEvaporatorColor = UnknownColor;

			FanCondensorInfo = UnknownText;
			FanCondensorColor = UnknownColor;

			ConcentratorAdvancedInfo = UnknownText;
			ConcentratorAdvancedColor = UnknownColor;

			CalculatedTemperatureSetting = UnknownText;

			IsMaster = true;
			IsSlave = true;
		}
	}
}
