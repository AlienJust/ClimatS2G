namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {

	internal interface IBvsReply65Telemetry {
		bool BvsInput1 { get; }
		bool BvsInput2 { get; }
		bool BvsInput3 { get; }
		bool BvsInput4 { get; }
		bool BvsInput5 { get; }
		bool BvsInput6 { get; }
		bool BvsInput7 { get; }
		bool BvsInput8 { get; }

		bool BvsInput9 { get; }
		bool BvsInput10 { get; }
		bool BvsInput11 { get; }
		bool BvsInput12 { get; }
		bool BvsInput13 { get; }
		bool BvsInput14 { get; }
		bool BvsInput15 { get; }
		bool BvsInput16 { get; }

		int Status { get; }
	}

	class BvsReply65Telemetry : IBvsReply65Telemetry {
		public bool BvsInput1 { get; set; }
		public bool BvsInput2 { get; set; }
		public bool BvsInput3 { get; set; }
		public bool BvsInput4 { get; set; }
		public bool BvsInput5 { get; set; }
		public bool BvsInput6 { get; set; }
		public bool BvsInput7 { get; set; }
		public bool BvsInput8 { get; set; }
		public bool BvsInput9 { get; set; }
		public bool BvsInput10 { get; set; }
		public bool BvsInput11 { get; set; }
		public bool BvsInput12 { get; set; }
		public bool BvsInput13 { get; set; }
		public bool BvsInput14 { get; set; }
		public bool BvsInput15 { get; set; }
		public bool BvsInput16 { get; set; }
		public int Status { get; set; }
	}
}