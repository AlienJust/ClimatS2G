namespace CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16 {
	public interface IRequest16Data {
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
