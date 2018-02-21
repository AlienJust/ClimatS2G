namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	internal sealed class MukWarmFloorDiagnostic1Simple : IMukWarmFloorDiagnostic1 {
		public bool ThermoResistorIsLost { get; }

		public MukWarmFloorDiagnostic1Simple(bool thermoResistorIsLost) {
			ThermoResistorIsLost = thermoResistorIsLost;
		}
	}
}