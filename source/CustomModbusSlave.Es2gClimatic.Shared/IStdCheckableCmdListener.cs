namespace CustomModbusSlave.Es2gClimatic.Shared {
	public interface IStdCheckableCmdListener {
		byte AddrToCheck { get; }
		byte CodeToCheck { get; }
		int Length { get; }
	}
}