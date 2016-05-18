namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IUshortToDoubleConverter {
		double? Convert(ushort? value);
	}
}