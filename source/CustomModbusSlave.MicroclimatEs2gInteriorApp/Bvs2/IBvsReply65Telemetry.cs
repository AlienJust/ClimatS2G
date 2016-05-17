namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {

	internal interface IBvs2Reply65Telemetry {
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
}