using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	class SystemDiagnosticViewModel : ViewModelBase {
		private const string UnknownText = "Неизвестно";
		private readonly IThreadNotifier _uiNotifier;
		private readonly ICmdListener<IMukVaporizerRequest16InteriorData> _cmdListenerMukVaporizerRequest16;
		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;
		private string _segmentType;
		private string _version;
		private string _workStage;

		public SystemDiagnosticViewModel(IThreadNotifier uiNotifier, ICmdListener<IMukVaporizerRequest16InteriorData> cmdListenerMukVaporizerRequest16, ICmdListener<IList<BytesPair>> cmdListenerKsm) {
			_uiNotifier = uiNotifier;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerKsm = cmdListenerKsm;

			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
			_cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;

			_segmentType = UnknownText;
			_version = UnknownText;
			_workStage = UnknownText;
		}

		private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data) {
			Version = new TextFormatterDotted("i", UnknownText).Format(data[34]);
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

	}
}
