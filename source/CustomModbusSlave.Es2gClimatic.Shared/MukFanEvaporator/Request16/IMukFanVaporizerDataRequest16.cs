namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public interface IMukFanVaporizerDataRequest16 {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeed { get; }
		int DeltaT { get; }
		double DeltaTSetting { get; }

		/// <summary>
		/// Секция: мастер или слейв
		/// </summary>
		bool IsSlave { get; }
	}
}
