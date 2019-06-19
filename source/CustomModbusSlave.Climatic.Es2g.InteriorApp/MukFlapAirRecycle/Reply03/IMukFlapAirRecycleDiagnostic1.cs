namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	interface IMukFlapAirRecycleDiagnostic1 {
		/// <summary>
		/// ошибка обмена с драйвером высоковольтного ключа
		/// </summary>
		bool HighVoltageKeyDriverLinkError { get; }
		bool SensorOneWire1Error { get; }
		bool SensorOneWire2Error { get; }
	}
}
