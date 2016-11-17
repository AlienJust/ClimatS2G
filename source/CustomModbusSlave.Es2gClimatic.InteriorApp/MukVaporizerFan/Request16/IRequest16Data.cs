namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeed { get; }
		int DeltaT { get; }

		int Reserve23 { get; }
	}
}
