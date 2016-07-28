namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	internal interface IKsmCommandWorkmode {
		KsmCommandWorkmode ParsedValue { get; }
		int RawValue { get; }
	}
}