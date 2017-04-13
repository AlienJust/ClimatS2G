using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	class SystemDiagnosticViewModel : ViewModelBase {
		private const string UnknownText = "Неизвестно";
		private const string NoLinkText = "Нет связи";
		private readonly IThreadNotifier _uiNotifier;
		private readonly ICmdListener<IMukFlapReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukVaporizerRequest16InteriorData> _cmdListenerMukVaporizerRequest16;
		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;

		private string _segmentType;
		private string _version;
		private string _workStage;
		private string _mukInfo2;

		public SystemDiagnosticViewModel(IThreadNotifier uiNotifier,
			ICmdListener<IMukFlapReply03Telemetry> cmdListenerMukFlapOuterAirReply03,
			ICmdListener<IMukVaporizerRequest16InteriorData> cmdListenerMukVaporizerRequest16, 
			ICmdListener<IList<BytesPair>> cmdListenerKsm) {

			_uiNotifier = uiNotifier;
			
			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerKsm = cmdListenerKsm;

			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
			_cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;

			ResetVmPropsToDefaultValues();
		}

		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReply03Telemetry data) {
			MukInfo2 = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);
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


		void ResetVmPropsToDefaultValues() {
			SegmentType = UnknownText;
			Version = UnknownText;
			WorkStage = UnknownText;
			MukInfo2 = NoLinkText;
		}
	}
}
