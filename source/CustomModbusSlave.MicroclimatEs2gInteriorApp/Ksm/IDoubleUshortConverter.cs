namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IDoubleUshortConverter {
		ushort ConvertToUshort(double value);
		double ConvertToDouble(ushort value);
	}
}