namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16 {
	/// <summary>
	/// Байт - регистр признаков
	/// </summary>
	internal interface IAttributesRegistryByte {
		/// <summary>
		/// b.0 = 1 салон - мастер
		/// </summary>
		bool SalonMaster { get; }
	}
}