namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {

	/// <summary>
	/// Данные запроса от КСМ к МУК заслонки рециркуляционного воздуха
	/// </summary>
	interface IMukFlapAirRecycleRequest16Data {
		int MukFlapAirRecycleWorkmodeCommandFromKsm { get; }
		int TemperatureOuterAir { get; }
		double TemperatureInnerAir { get; }
		int FanLevelSetting { get; }
		int Reserve623 { get; }
		int Reserve624 { get; }
	}
}
