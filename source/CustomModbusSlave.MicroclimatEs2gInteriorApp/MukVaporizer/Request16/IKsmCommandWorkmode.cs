namespace CustomModbusSlave.MicroclimatEs2gApp.MukVaporizer.Request16 {
	internal interface IKsmCommandWorkmode {
		bool AutomaticMode { get; }
		// zb bit #2 is skipped
		bool ForceHeatRegulator { get; }
		bool ForceHeatModePwm100 { get; }
		bool ForceCoolMode { get; }
		bool HeatModeAndCoolModeAreOff { get; }
		bool ManualMode { get; }
		bool ForceHeatModePwm50 { get; }
	}
}