namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IDoubleUshortConverter {
		ushort ConvertToUshort(double value);
		double ConvertToDouble(ushort value);
	}

	class DoubleUshortConverterSimple : IDoubleUshortConverter {
		public ushort ConvertToUshort(double value) {
			return (ushort) value;
		}

		public double ConvertToDouble(ushort value) {
			return value;
		}
	}
}