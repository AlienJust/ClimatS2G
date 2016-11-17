namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		int TargetTemperature { get; }
		int FanSpeed { get; }

		bool IsInteriorIfTrueCabinIfFalse { get; }

		int Reserve26 { get; }
	}
}
