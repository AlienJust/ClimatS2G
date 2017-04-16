using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	class SystemDiagnosticViewModel : ViewModelBase {
		private const string UnknownText = "Неизвестно";
		private const string NoLinkText = "Нет связи";
		private readonly IThreadNotifier _uiNotifier;
		private readonly ICmdListener<IMukFlapReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukVaporizerFanReply03Telemetry> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukVaporizerRequest16InteriorData> _cmdListenerMukVaporizerRequest16;
		private readonly ICmdListener<IMukFridgeFanReply03Data> _cmdListenerMukFridgeFanReply03;
		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;

		private string _segmentType;
		private string _version;
		private string _workStage;
		private string _mukInfo2;
		private string _mukInfo3;
		private string _mukInfo4;
		private string _mukInfo6;
		private string _mukInfo7;
		private string _mukInfo8;

		public SystemDiagnosticViewModel(IThreadNotifier uiNotifier,
			ICmdListener<IMukFlapReply03Telemetry> cmdListenerMukFlapOuterAirReply03,
			ICmdListener<IMukVaporizerFanReply03Telemetry> cmdListenerMukVaporizerReply03,
			ICmdListener<IMukVaporizerRequest16InteriorData> cmdListenerMukVaporizerRequest16,
			ICmdListener<IMukFridgeFanReply03Data> cmdListenerMukFridgeFanReply03,
			ICmdListener<IMukAirExhausterReply03Data> cmdListenerMukAirExhausterReply03,
			ICmdListener<IMukFlapReturnAirReply03Telemetry> cmdListenerMukFlapReturnAirReply03,
			ICmdListener<IMukFlapWinterSummerReply03Telemetry> cmdListenerMukFlapWinterSummerReply03,
			ICmdListener<IList<BytesPair>> cmdListenerKsm) {

			_uiNotifier = uiNotifier;
			
			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerMukFridgeFanReply03 = cmdListenerMukFridgeFanReply03;
			_cmdListenerMukAirExhausterReply03 = cmdListenerMukAirExhausterReply03;
			_cmdListenerMukFlapReturnAirReply03 = cmdListenerMukFlapReturnAirReply03;
			_cmdListenerMukFlapWinterSummerReply03 = cmdListenerMukFlapWinterSummerReply03;
			_cmdListenerKsm = cmdListenerKsm;

			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
			_cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;

			ResetVmPropsToDefaultValues();
		}
		
		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReply03Telemetry data) {
			MukInfo2 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
		}

		private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukVaporizerFanReply03Telemetry data) {
			MukInfo3 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
		}

		private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data) {
			Version = new TextFormatterDotted(UnknownText).Format(data[34]);
			WorkStage = new TextFormatterWorkStage().Format(data[8]);
		}

		private void CmdListenerMukVaporizerRequest16DataReceived(IList<byte> bytes, IMukVaporizerRequest16InteriorData data) {
			_uiNotifier.Notify(() => { SegmentType = data == null ? UnknownText : data.IsSlave ? "Slave" : "Master"; });
		}



		public string SegmentType {
			get { return _segmentType; }
			set {
				if (_segmentType != value) {
					_segmentType = value;
					RaisePropertyChanged(()=> SegmentType);
				}
			}
		}

		public string Version {
			get { return _version; }
			set {
				if (_version != value) {
					_version = value;
					RaisePropertyChanged(()=>Version);
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
					RaisePropertyChanged(()=>MukInfo2);
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

		public string MukInfo4 {
			get { return _mukInfo4; }
			set {
				if (_mukInfo4 != value) {
					_mukInfo4 = value;
					RaisePropertyChanged(() => MukInfo4);
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

		public string MukInfo7 {
			get { return _mukInfo3; }
			set {
				if (_mukInfo7 != value) {
					_mukInfo7 = value;
					RaisePropertyChanged(() => MukInfo7);
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


		void ResetVmPropsToDefaultValues() {
			SegmentType = UnknownText;
			Version = UnknownText;
			WorkStage = UnknownText;
			MukInfo2 = NoLinkText;
			MukInfo3 = NoLinkText;
			MukInfo4 = NoLinkText;
			MukInfo6 = NoLinkText;
			MukInfo7 = NoLinkText;
			MukInfo8 = NoLinkText;
		}
	}
}
