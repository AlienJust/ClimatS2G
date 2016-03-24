namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal interface IBsSmState {
		bool NoLinkMk1AndMspuD1 { get; }
		bool NoLinkMk2AndMspuD2 { get; }
		bool ReliabilityBitFromMspuD1 { get; }
		bool ReliabilityBitFromMspuD2 { get; }
		bool MspuD1ReceiptNumberFail { get; }
		bool MspuD2ReceiptNumberFail { get; }
		bool NoLinkMk1BySpi { get; }
		bool Reserved { get; }
	}
}