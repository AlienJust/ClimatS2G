namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	public interface IUshortToDoubleConverter {
		double? Convert(ushort? value);
	}
}