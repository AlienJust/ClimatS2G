namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableParameter {
		ushort? ReceivedUshortValue { get; set; }
		double? ReceivedDoubleValue { get; set; }
	}
}