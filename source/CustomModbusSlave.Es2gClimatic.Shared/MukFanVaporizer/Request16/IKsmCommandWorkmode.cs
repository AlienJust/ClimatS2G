namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public interface IKsmCommandWorkmode {
		bool AutomaticMode { get; }
		// zb bit #2 is skipped
		bool ForceHeatRegulator { get; }
		bool ForceHeatModePwm100 { get; }
		bool ForceCoolMode { get; }
		bool HeatModeAndCoolModeAreOff { get; }
		bool ManualMode { get; }
		bool ForceHeatModePwm50 { get; }

		/// <summary>
		/// Принудительное отключение Emerson по низкому давлению
		/// </summary>
		bool ForceEmersonOffByLowPressure { get; }

		/// <summary>
		/// конфигурация работы
		/// </summary>
		int WorkConfiguration { get; }
	}
}