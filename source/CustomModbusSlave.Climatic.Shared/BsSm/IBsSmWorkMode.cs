namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	/// <summary>
	/// Режим работы
	/// </summary>
	public interface IBsSmWorkMode {
		bool RelabilityFlag { get; }
		bool PowerLimitation { get; }
		bool Station { get; }
		bool Tunnel { get; }
		bool LongDistanceJourney { get; }
		bool HasVoltage3000V { get; }
		bool HasVoltage380V { get; }
		bool IsCompressorSwitchOnPermitted { get; }
	}
}
