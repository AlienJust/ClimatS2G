namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Request16 {

	/// <summary>
	/// Данные запроса от КСМ к МУК вытяжного вентилятора
	/// </summary>
	interface IMukFanAirExhausterRequest16Data {
		//MukFanAirExhausterWorkmodeCommandFromKsmAsInteger
		IMukFanAirExhausterWorkmodeCommandFromKsm MukFanAirExhausterWorkmodeCommandFromKsm { get; }
		int TemperatureOuterAir { get; }
		//AttributesRegistryByteAsInteger
		IAttributesRegistryByte AttributesRegistryByte { get; }
		int FanLevelSetting { get; }
		int Reserve517 { get; }
		int Reserve518 { get; }
	}
}
