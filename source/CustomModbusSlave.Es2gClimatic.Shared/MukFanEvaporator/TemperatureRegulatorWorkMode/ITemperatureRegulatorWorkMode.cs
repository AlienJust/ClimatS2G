namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode
{
	/// <summary>
	/// Режим работы регулятора температуры
	/// </summary>
	public interface ITemperatureRegulatorWorkMode {
		/// <summary>
		/// Полное значение (пара байт)
		/// </summary>
		int FullValue { get; }
		/// <summary>
		/// режим «охлаждение», иначе режим «нагрев»
		/// </summary>
		bool Cool { get; }
		/// <summary>
		/// режим ограничения охлаждения/нагрева по внешней температуре
		/// </summary>
		bool Restrict { get; }
	}
}