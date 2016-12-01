using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	/// <summary>
	/// Режим работы №1 (ответ БС-СМ)
	/// </summary>
	interface IWorkmode1 {
		/// <summary>
		/// Целевая температура в пассажирском салоне
		/// </summary>
		int InnerTemperature { get; }

		/// <summary>
		/// Режим работы климатической системы
		/// </summary>
		ClimaticSystemWorkMode ClimaticWorkMode { get; }
	}
}
