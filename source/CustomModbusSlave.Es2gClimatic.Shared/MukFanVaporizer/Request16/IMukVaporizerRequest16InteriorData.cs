namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public interface IMukVaporizerRequest16InteriorData {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeed { get; }
		int DeltaT { get; }
		int Reserve23 { get; }

		/// <summary>
		/// Секция: мастер или слейв
		/// </summary>
		bool IsSlave { get; }
	}
}
