using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease {
	class MukFlapOuterAirOuterAirDiagnostic2 : IMukFlapOuterAirDiagnostic2 {
		public MukFlapOuterAirOuterAirDiagnostic2(bool innormalPressureValueCircuit1, bool innormalTemperatureValueCircuit1, bool innormalPressureValueCircuit2, bool innormalTemperatureValueCircuit2, bool flapMotorWorkInversed, bool osShowsFlapDoesNotReachLimitPositions, bool osShowsFlapDoesNotReach50Percent) {
			
			InnormalPressureValueCircuit1 = innormalPressureValueCircuit1;
			InnormalTemperatureValueCircuit1 = innormalTemperatureValueCircuit1;
			InnormalPressureValueCircuit2 = innormalPressureValueCircuit2;
			InnormalTemperatureValueCircuit2 = innormalTemperatureValueCircuit2;
			FlapMotorWorkInversed = flapMotorWorkInversed;
			OsShowsFlapDoesNotReachLimitPositions = osShowsFlapDoesNotReachLimitPositions;
			OsShowsFlapDoesNotReach50Percent = osShowsFlapDoesNotReach50Percent;
		}

		public bool InnormalPressureValueCircuit1 { get; }
		public bool InnormalTemperatureValueCircuit1 { get; }
		public bool InnormalPressureValueCircuit2 { get; }
		public bool InnormalTemperatureValueCircuit2 { get; }
		public bool FlapMotorWorkInversed { get; }
		public bool OsShowsFlapDoesNotReachLimitPositions { get; }
		public bool OsShowsFlapDoesNotReach50Percent { get; }
		
	}
}
