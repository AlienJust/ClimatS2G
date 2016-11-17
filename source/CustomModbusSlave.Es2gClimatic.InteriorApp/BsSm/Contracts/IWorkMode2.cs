using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts {
	internal interface IWorkMode2 {
		ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		bool SlaveRegulatorOnByMaster { get; }
	}
}