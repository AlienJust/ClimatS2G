namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16 {
	/// <summary>
	/// Полученная от КСМ команда-режим работы вентилятора вытяжки воздуха
	/// </summary>
	internal interface IMukFanAirExhausterWorkmodeCommandFromKsm {
		/// <summary>
		/// D0 работа (регулятор работает)
		/// </summary>
		bool RegulatorIsWorking { get; }
	}
}