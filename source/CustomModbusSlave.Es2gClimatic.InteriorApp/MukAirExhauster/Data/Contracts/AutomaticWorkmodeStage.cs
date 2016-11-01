namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	internal enum AutomaticWorkmodeStage {
		ControllerInitialization, // 0
		WaitingForPowerOnCommand, // 1
		WorkingWithFanOnByTable, // 2
		Unknown
	}
}