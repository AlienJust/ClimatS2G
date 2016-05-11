namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal interface IWorkMode2 {
		ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		bool SlaveRegulatorOnByMaster { get; }
	}
}