namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan.Request16 {
	internal class KsmCommandWorkmodeSimple : IKsmCommandWorkmode {
		public bool AutomaticMode { get; set; }
		public bool ForceHeatRegulator { get; set; }
		public bool ForceHeatModePwm100 { get; set; }
		public bool ForceCoolMode { get; set; }
		public bool HeatModeAndCoolModeAreOff { get; set; }
		public bool ManualMode { get; set; }
		public bool ForceHeatModePwm50 { get; set; }
	}
}