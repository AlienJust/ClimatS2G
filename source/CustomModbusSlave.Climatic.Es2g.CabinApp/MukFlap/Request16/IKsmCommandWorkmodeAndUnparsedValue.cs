namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	internal interface IKsmCommandWorkmodeAndUnparsedValue {
		KsmCommandWorkmode ParsedValue { get; }
		int RawValue { get; }
	}
}
