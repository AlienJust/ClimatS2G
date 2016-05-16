using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm.BsSmState;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm.Contracts {
	internal interface IBsSmAndKsm1DataCommand32Reply {
		uint AstronomicTime { get; }
		uint DelayedStartTime { get; }
		int TemperatureOutdoor { get; }

		int CarType { get; }
		int Reserve13D4D7 { get; }

		//byte 14:
		int TargetTemperatureInterior { get; }
		int ClimaticSystemWorkmode14D4D7 { get; }

		//byte 15:
		IWorkModeReply WorkModeAndCompressorSwitch { get; }
		
		int AllowedPowerConsuptionBy380Vline { get; }
		int Reserve17 { get; }
		int Reserve18 { get; }

		//int Segment2CurentCalculatedPowerConsumptionBy380Vline { get; }
		//int Segment2InteriorTemperature { get; }

		// from one based byte 19 upto 40 inclusive
		IBsSmAndKsm1DataCommand32Request Ksm2Request { get; }


		IBsSmState BsSmState { get; }
		int BsSmVersionNumber { get; }

		int Reserve43 { get; }
		int Reserve44 { get; }
		int Reserve45 { get; }

	}
}