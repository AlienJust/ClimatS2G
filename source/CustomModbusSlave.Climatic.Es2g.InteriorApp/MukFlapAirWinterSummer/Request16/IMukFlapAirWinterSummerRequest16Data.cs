namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16 {

	/// <summary>
	/// Данные запроса от КСМ к МУК заслонки рециркуляционного воздуха
	/// </summary>
	interface IMukFlapAirWinterSummerRequest16Data {
		int MukFlapAirWinterSummerWorkmodeCommandFromKsm { get; }
		int TemperatureOuterAir { get; }
		double TemperatureInnerAir { get; }
		int FanLevelSetting { get; }
		int Reserve725 { get; }
		int Reserve726 { get; }
	}
}
