namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts {
	internal enum AutomaticWorkmodeStage {
		ControllerInitialization, // 0
		WaitingForPowerOnCommand, // 1
		WorkingWithFanOnByTable, // 2
		Unknown
	}
}