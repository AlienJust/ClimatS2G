﻿namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeed { get; }
		int Reserve21 { get; }
		int Reserve22 { get; }
	}
}