namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	public enum MukFanEvaporatorWorkstage {
		ControllerInit,
		FanTest,
		WorkAndFanIsGood,
		WorkAndFanIsBad,
		AllSwitchesAndPwmAreCauseNoTemperatureData,
		Unknown
	}
}