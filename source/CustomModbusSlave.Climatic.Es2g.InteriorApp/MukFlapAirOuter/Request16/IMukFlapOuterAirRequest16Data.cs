namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Request16 {
	interface IMukFlapOuterAirRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		int TargetTemperature { get; }
		int FanSpeed { get; }

		bool IsInteriorIfTrueCabinIfFalse { get; }

		int Reserve26 { get; }
	}
}
