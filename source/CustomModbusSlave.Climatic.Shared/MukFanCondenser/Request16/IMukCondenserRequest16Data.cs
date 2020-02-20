﻿namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16 {
	public interface IMukCondenserRequest16Data {
		IMukCondenserWorkmodeCommandFromKsm MukCondenserWorkmodeCommandFromKsm { get; }
		int PressureSetting { get; }
		int Reserve14 { get; }
	}
}
