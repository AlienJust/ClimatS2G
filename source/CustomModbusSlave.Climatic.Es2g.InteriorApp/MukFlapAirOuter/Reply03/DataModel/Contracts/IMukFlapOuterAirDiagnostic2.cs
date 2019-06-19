namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts {
	interface IMukFlapOuterAirDiagnostic2 {
		bool InnormalPressureValueCircuit1 { get; }
		bool InnormalTemperatureValueCircuit1 { get; }
		bool InnormalPressureValueCircuit2 { get; }
		bool InnormalTemperatureValueCircuit2 { get; }
		/// <summary>
		/// Инверсная работа привода заслонки
		/// </summary>
		bool FlapMotorWorkInversed { get; }
		bool OsShowsFlapDoesNotReachLimitPositions { get; }
		bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
