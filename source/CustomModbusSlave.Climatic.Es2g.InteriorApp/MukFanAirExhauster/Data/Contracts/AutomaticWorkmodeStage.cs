namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts {
	internal enum AutomaticWorkmodeStage {
		ControllerInitialization, // 0
		WaitingForPowerOnCommand, // 1
		WorkingWithFanOnByTable, // 2
		Unknown
	}
}
