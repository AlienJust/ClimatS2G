using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {
	class BvsDataViewModel : ViewModelBase, ICommandListener, IBvsReply65Telemetry {
		private readonly IThreadNotifier _notifier;
		private IBvsReply65Telemetry _reply65Telemetry;

		BvsDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr == 0x1E && code == 0x41) {
				var reply65Builder = new BvsReply65TelemetryBuilder(data).Build();
			}
		}

		public bool BvsInput1 {
			get {
				return _reply65Telemetry.BvsInput1;
			}
		}

		public bool BvsInput2 {
			get {
				return _reply65Telemetry.BvsInput2;
			}
		}

		public bool BvsInput3 {
			get {
				return _reply65Telemetry.BvsInput3;
			}
		}

		public bool BvsInput4 {
			get {
				return _reply65Telemetry.BvsInput4;
			}
		}

		public bool BvsInput5 {
			get {
				return _reply65Telemetry.BvsInput5;
			}
		}

		public bool BvsInput6 {
			get {
				return _reply65Telemetry.BvsInput6;
			}
		}

		public bool BvsInput7 {
			get {
				return _reply65Telemetry.BvsInput7;
			}
		}

		public bool BvsInput8 {
			get {
				return _reply65Telemetry.BvsInput8;
			}
		}

		public bool BvsInput9 {
			get {
				return _reply65Telemetry.BvsInput9;
			}
		}

		public bool BvsInput10 {
			get {
				return _reply65Telemetry.BvsInput10;
			}
		}

		public bool BvsInput11 {
			get {
				return _reply65Telemetry.BvsInput11;
			}
		}

		public bool BvsInput12 {
			get {
				return _reply65Telemetry.BvsInput12;
			}
		}

		public bool BvsInput13 {
			get {
				return _reply65Telemetry.BvsInput13;
			}
		}

		public bool BvsInput14 {
			get {
				return _reply65Telemetry.BvsInput14;
			}
		}

		public bool BvsInput15 {
			get {
				return _reply65Telemetry.BvsInput15;
			}
		}

		public bool BvsInput16 {
			get {
				return _reply65Telemetry.BvsInput16;
			}
		}

		public int Status { get { return _reply65Telemetry.Status; } }
	}
}
