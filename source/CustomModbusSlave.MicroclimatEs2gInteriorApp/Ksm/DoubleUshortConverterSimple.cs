namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class DoubleUshortConverterSimple : IDoubleUshortConverter {
		public ushort ConvertToUshort(double value) {
			return (ushort) value;
		}

		public double ConvertToDouble(ushort value) {
			return value;
		}
	}
}