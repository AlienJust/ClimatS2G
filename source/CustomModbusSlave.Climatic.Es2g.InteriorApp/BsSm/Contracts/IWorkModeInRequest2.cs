namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts {
	internal interface IWorkModeInRequest2 {
		Shared.BsSm.ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		bool SlaveRegulatorOnByMaster { get; }
	}
}
