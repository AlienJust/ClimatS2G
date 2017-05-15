using System;
using System.Collections.Generic;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
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
		private const Colors NoSensorColor = Colors.OrangeRed;

		private const Colors OkLinkColor = Colors.LimeGreen;
		private const Colors OkSensorColor = Colors.LimeGreen;


		private readonly IThreadNotifier _uiNotifier;
		private readonly ICmdListener<IMukFlapReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukFridgeFanReply03;
		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Reply> _cmdListenerBsSmReply32;

		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;

		private string _segmentType;
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

		public SystemDiagnosticViewModel(IThreadNotifier uiNotifier,
			ICmdListener<IMukFlapReply03Telemetry> cmdListenerMukFlapOuterAirReply03,
			ICmdListener<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
			ICmdListener<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16,
			ICmdListener<IMukCondensorFanReply03Data> cmdListenerMukFridgeFanReply03,
			ICmdListener<IMukAirExhausterReply03Data> cmdListenerMukAirExhausterReply03,
			ICmdListener<IMukFlapReturnAirReply03Telemetry> cmdListenerMukFlapReturnAirReply03,
			ICmdListener<IMukFlapWinterSummerReply03Telemetry> cmdListenerMukFlapWinterSummerReply03,
			ICmdListener<IBsSmAndKsm1DataCommand32Reply> cmdListenerBsSmReply32,
			ICmdListener<IList<BytesPair>> cmdListenerKsm) {

			_uiNotifier = uiNotifier;

			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerMukFridgeFanReply03 = cmdListenerMukFridgeFanReply03;
			_cmdListenerMukAirExhausterReply03 = cmdListenerMukAirExhausterReply03;
			_cmdListenerMukFlapReturnAirReply03 = cmdListenerMukFlapReturnAirReply03;
			_cmdListenerMukFlapWinterSummerReply03 = cmdListenerMukFlapWinterSummerReply03;
			_cmdListenerBsSmReply32 = cmdListenerBsSmReply32;
			_cmdListenerKsm = cmdListenerKsm;

			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
			_cmdListenerMukFridgeFanReply03.DataReceived += CmdListenerMukFridgeFanReply03OnDataReceived;
			_cmdListenerMukAirExhausterReply03.DataReceived += CmdListenerMukAirExhausterReply03OnDataReceived;
			_cmdListenerMukFlapReturnAirReply03.DataReceived += CmdListenerMukFlapReturnAirReply03OnDataReceived;
			_cmdListenerMukFlapWinterSummerReply03.DataReceived += CmdListenerMukFlapWinterSummerReply03OnDataReceived;
			_cmdListenerBsSmReply32.DataReceived += CmdListenerBsSmReply32OnDataReceived;
			_cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;



			ResetVmPropsToDefaultValues();
		}

		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo2 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
				MukInfoColor2 = OkLinkColor;

				if (data.Diagnostic1.NoEmersionControllerAnswer) {
					EmersonInfo = NoLinkText;
					EmersonInfoColor = NoLinkColor;
				}
				else {
					EmersonInfo = OkLinkText;
					EmersonInfoColor = OkLinkColor;
				}
			});
		}

		private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data) {
			_uiNotifier.Notify(() => {
				MukInfo3 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
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

			});
		}

		private void CmdListenerMukFridgeFanReply03OnDataReceived(IList<byte> bytes, IMukCondensorFanReply03Data data) {
			_uiNotifier.Notify(() => {
				MukInfo4 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
				MukInfoColor4 = OkLinkColor;
			});
		}

		private void CmdListenerMukAirExhausterReply03OnDataReceived(IList<byte> bytes, IMukAirExhausterReply03Data data) {
			_uiNotifier.Notify(() => {
				MukInfo6 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
				MukInfoColor6 = OkLinkColor;
			});
		}

		private void CmdListenerMukFlapReturnAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReturnAirReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo7 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
				MukInfoColor7 = OkLinkColor;
			});
		}

		private void CmdListenerMukFlapWinterSummerReply03OnDataReceived(IList<byte> bytes, IMukFlapWinterSummerReply03Telemetry data) {
			_uiNotifier.Notify(() => {
				MukInfo8 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
				MukInfoColor8 = OkLinkColor;
			});
		}

		private void CmdListenerBsSmReply32OnDataReceived(IList<byte> bytes, IBsSmAndKsm1DataCommand32Reply data) {
			_uiNotifier.Notify(() => {
				BsSmInfo = new TextFormatterIntegerDotted().Format(data.BsSmVersionNumber);
				BsSmInfoColor = OkLinkColor;
			});
		}


		private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data) {
			_uiNotifier.Notify(() => {
				Version = new TextFormatterDotted(UnknownText).Format(data[34]);
				WorkStage = new TextFormatterWorkStage().Format(data[8]);

				if (data[22].HighFirstUnsignedValue.GetBit(0)) {
					MukInfo2 = NoLinkText;
					MukInfoColor2 = NoLinkColor;

					EmersonInfo = NoLinkText;
					EmersonInfoColor = NoLinkColor;
				}

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
				}

				if (data[22].HighFirstUnsignedValue.GetBit(4)) {
					MukInfo4 = NoLinkText;
					MukInfoColor4 = NoLinkColor;
				}

				if (data[22].HighFirstUnsignedValue.GetBit(6)) {
					MukInfo6 = NoLinkText;
					MukInfoColor6 = NoLinkColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(0)) {
					MukInfo7 = NoLinkText;
					MukInfoColor7 = NoLinkColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(2)) {
					MukInfo8 = NoLinkText;
					MukInfoColor8 = NoLinkColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(4)) {
					BsSmInfo = NoLinkText;
					BsSmInfoColor = NoLinkColor;
				}

				if (data[23].HighFirstUnsignedValue.GetBit(5)) {
					BvsInfo1 = NoLinkText;
					BvsInfoColor1 = NoLinkColor;
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
			_uiNotifier.Notify(() => { SegmentType = data == null ? UnknownText : data.IsSlave ? "Slave" : "Master"; });
		}



		public string SegmentType {
			get { return _segmentType; }
			set {
				if (_segmentType != value) {
					_segmentType = value;
					RaisePropertyChanged(() => SegmentType);
				}
			}
		}

		public string Version {
			get { return _version; }
			set {
				if (_version != value) {
					_version = value;
					RaisePropertyChanged(() => Version);
				}
			}
		}

		public string WorkStage {
			get { return _workStage; }
			set {
				if (_workStage != value) {
					_workStage = value;
					RaisePropertyChanged(() => WorkStage);
				}
			}
		}


		public string MukInfo2 {
			get { return _mukInfo2; }
			set {
				if (_mukInfo2 != value) {
					_mukInfo2 = value;
					RaisePropertyChanged(() => MukInfo2);
				}
			}
		}
		public Colors MukInfoColor2 {
			get { return _mukInfoColor2; }
			set {
				if (_mukInfoColor2 != value) {
					_mukInfoColor2 = value;
					RaisePropertyChanged(() => MukInfoColor2);
				}
			}
		}


		public string MukInfo3 {
			get { return _mukInfo3; }
			set {
				if (_mukInfo3 != value) {
					_mukInfo3 = value;
					RaisePropertyChanged(() => MukInfo3);
				}
			}
		}
		public Colors MukInfoColor3 {
			get { return _mukInfoColor3; }
			set {
				if (_mukInfoColor3 != value) {
					_mukInfoColor3 = value;
					RaisePropertyChanged(() => MukInfoColor3);
				}
			}
		}


		public string MukInfo4 {
			get { return _mukInfo4; }
			set {
				if (_mukInfo4 != value) {
					_mukInfo4 = value;
					RaisePropertyChanged(() => MukInfo4);
				}
			}
		}
		public Colors MukInfoColor4 {
			get { return _mukInfoColor4; }
			set {
				if (_mukInfoColor4 != value) {
					_mukInfoColor4 = value;
					RaisePropertyChanged(() => MukInfoColor4);
				}
			}
		}


		public string MukInfo6 {
			get { return _mukInfo6; }
			set {
				if (_mukInfo6 != value) {
					_mukInfo6 = value;
					RaisePropertyChanged(() => MukInfo6);
				}
			}
		}
		public Colors MukInfoColor6 {
			get { return _mukInfoColor6; }
			set {
				if (_mukInfoColor6 != value) {
					_mukInfoColor6 = value;
					RaisePropertyChanged(() => MukInfoColor6);
				}
			}
		}


		public string MukInfo7 {
			get { return _mukInfo7; }
			set {
				if (_mukInfo7 != value) {
					_mukInfo7 = value;
					RaisePropertyChanged(() => MukInfo7);
				}
			}
		}
		public Colors MukInfoColor7 {
			get { return _mukInfoColor7; }
			set {
				if (_mukInfoColor7 != value) {
					_mukInfoColor7 = value;
					RaisePropertyChanged(() => MukInfoColor7);
				}
			}
		}


		public string MukInfo8 {
			get { return _mukInfo8; }
			set {
				if (_mukInfo8 != value) {
					_mukInfo8 = value;
					RaisePropertyChanged(() => MukInfo8);
				}
			}
		}
		public Colors MukInfoColor8 {
			get { return _mukInfoColor8; }
			set {
				if (_mukInfoColor8 != value) {
					_mukInfoColor8 = value;
					RaisePropertyChanged(() => MukInfoColor8);
				}
			}
		}


		public string BsSmInfo {
			get { return _bsSmInfo; }
			set {
				if (_bsSmInfo != value) {
					_bsSmInfo = value;
					RaisePropertyChanged(() => BsSmInfo);
				}
			}
		}
		public Colors BsSmInfoColor {
			get { return _bsSmInfoColor; }
			set {
				if (_bsSmInfoColor != value) {
					_bsSmInfoColor = value;
					RaisePropertyChanged(() => BsSmInfoColor);
				}
			}
		}


		public string BvsInfo1 {
			get { return _bvsInfo1; }
			set {
				if (_bvsInfo1 != value) {
					_bvsInfo1 = value;
					RaisePropertyChanged(() => BvsInfo1);
				}
			}
		}
		public Colors BvsInfoColor1 {
			get { return _bvsInfoColor1; }
			set {
				if (_bvsInfoColor1 != value) {
					_bvsInfoColor1 = value;
					RaisePropertyChanged(() => BvsInfoColor1);
				}
			}
		}


		public string BvsInfo2 {
			get { return _bvsInfo2; }
			set {
				if (_bvsInfo2 != value) {
					_bvsInfo2 = value;
					RaisePropertyChanged(() => BvsInfo2);
				}
			}
		}
		public Colors BvsInfoColor2 {
			get { return _bvsInfoColor2; }
			set {
				if (_bvsInfoColor2 != value) {
					_bvsInfoColor2 = value;
					RaisePropertyChanged(() => BvsInfoColor2);
				}
			}
		}


		public string EmersonInfo {
			get { return _emersonInfo; }
			set {
				if (_emersonInfo != value) {
					_emersonInfo = value;
					RaisePropertyChanged(() => EmersonInfo);
				}
			}
		}
		public Colors EmersonInfoColor {
			get { return _emersonInfoColor; }
			set {
				if (_emersonInfoColor != value) {
					_emersonInfoColor = value;
					RaisePropertyChanged(() => EmersonInfoColor);
				}
			}
		}



		public string EvaporatorFanControllerInfo {
			get { return _evaporatorFanControllerInfo; }
			set {
				if (_evaporatorFanControllerInfo != value) {
					_evaporatorFanControllerInfo = value;
					RaisePropertyChanged(() => EvaporatorFanControllerInfo);
				}
			}
		}
		public Colors EvaporatorFanControllerInfoColor {
			get { return _evaporatorFanControllerInfoColor; }
			set {
				if (_evaporatorFanControllerInfoColor != value) {
					_evaporatorFanControllerInfoColor = value;
					RaisePropertyChanged(() => EvaporatorFanControllerInfoColor);
				}
			}
		}



		public string SensorOuterAirInfo {
			get { return _sensorOuterAirInfo; }
			set {
				if (_sensorOuterAirInfo != value) {
					_sensorOuterAirInfo = value;
					RaisePropertyChanged(() => SensorOuterAirInfo);
				}
			}
		}
		public Colors SensorOuterAirInfoColor {
			get { return _sensorOuterAirInfoColor; }
			set {
				if (_sensorOuterAirInfoColor != value) {
					_sensorOuterAirInfoColor = value;
					RaisePropertyChanged(() => SensorOuterAirInfoColor);
				}
			}
		}


		public string SensorRecycleAirInfo {
			get { return _sensorRecycleAirInfo; }
			set {
				if (_sensorRecycleAirInfo != value) {
					_sensorRecycleAirInfo = value;
					RaisePropertyChanged(() => SensorRecycleAirInfo);
				}
			}
		}
		public Colors SensorRecycleAirInfoColor {
			get { return _sensorRecycleAirInfoColor; }
			set {
				if (_sensorRecycleAirInfoColor != value) {
					_sensorRecycleAirInfoColor = value;
					RaisePropertyChanged(() => SensorRecycleAirInfoColor);
				}
			}
		}
		

		#region Props Датчик подаваемого воздуха
		public string SensorSupplyAirInfo {
			get { return _sensorSupplyAirInfo; }
			set {
				if (_sensorSupplyAirInfo != value) {
					_sensorSupplyAirInfo = value;
					RaisePropertyChanged(() => SensorSupplyAirInfo);
				}
			}
		}
		public Colors SensorSupplyAirInfoColor {
			get { return _sensorSupplyAirInfoColor; }
			set {
				if (_sensorSupplyAirInfoColor != value) {
					_sensorSupplyAirInfoColor = value;
					RaisePropertyChanged(() => SensorSupplyAirInfoColor);
				}
			}
		}
		#endregion


		public string SensorInteriorAirInfo1 {
			get { return _sensorInteriorAirInfo1; }
			set {
				if (_sensorInteriorAirInfo1 != value) {
					_sensorInteriorAirInfo1 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfo1);
				}
			}
		}
		public Colors SensorInteriorAirInfoColor1 {
			get { return _sensorInteriorAirInfoColor1; }
			set {
				if (_sensorInteriorAirInfoColor1 != value) {
					_sensorInteriorAirInfoColor1 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfoColor1);
				}
			}
		}


		public string SensorInteriorAirInfo2 {
			get { return _sensorInteriorAirInfo2; }
			set {
				if (_sensorInteriorAirInfo2 != value) {
					_sensorInteriorAirInfo2 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfo2);
				}
			}
		}
		public Colors SensorInteriorAirInfoColor2 {
			get { return _sensorInteriorAirInfoColor2; }
			set {
				if (_sensorInteriorAirInfoColor2 != value) {
					_sensorInteriorAirInfoColor2 = value;
					RaisePropertyChanged(() => SensorInteriorAirInfoColor2);
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
		}
	}
}
