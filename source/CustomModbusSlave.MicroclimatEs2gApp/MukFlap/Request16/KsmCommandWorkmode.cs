namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	internal enum KsmCommandWorkmode {
		/// <summary>
		/// Выключено
		/// </summary>
		Off = 0,

		/// <summary>
		/// Работа в автоматическом режиме
		/// </summary>
		Auto = 1,

		/// <summary>
		/// Работа по ручной уставке
		/// </summary>
		Manual = 2,

		/// <summary>
		/// Неизвестное значение
		/// </summary>
		Unknown
	}
}