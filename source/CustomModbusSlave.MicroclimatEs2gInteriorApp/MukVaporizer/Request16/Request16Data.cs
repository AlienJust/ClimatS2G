﻿namespace CustomModbusSlave.MicroclimatEs2gApp.MukVaporizer.Request16 {
	class Request16Data : IRequest16Data {
		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; set; }
		public int OuterTemperature { get; set; }
		public double InnerTemperature { get; set; }
		public int FanSpeed { get; set; }
		public int DeltaT { get; set; }
		public int Reserve23 { get; set; }
	}
}