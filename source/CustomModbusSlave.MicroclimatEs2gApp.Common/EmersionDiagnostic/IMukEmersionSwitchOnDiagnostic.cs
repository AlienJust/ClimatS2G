namespace CustomModbusSlave.MicroclimatEs2gApp.Common.EmersionDiagnostic {
	public interface IMukEmersionSwitchOnDiagnostic {
		/// <summary>
		/// МУК выдает включение на Emerson
		/// </summary>
		bool MukIsSwitchingEmersionOn { get; }

		/// <summary>
		/// МУК выдерживает таймаут включение Emerson (5 минут после последнего включение)
		/// </summary>
		bool MukIsWatingFor5MinutesAfterLastSwitchingEmersionOn { get; }

		/// <summary>
		/// МУК выдерживает таймаут выключения Emerson (минимальные 5 минут работы)
		/// </summary>
		bool MukIsWaitingFor5MinutesWhileEmersionIsSwithedOn { get; }
	}
}
