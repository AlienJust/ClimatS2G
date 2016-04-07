﻿namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IOneWireSensorErrorCode {
		int AbsoluteValue { get; }
		OneWireSensorErrorCode KnownValue { get; }
	}
}