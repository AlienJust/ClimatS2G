using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class WorkMode2Simple : IWorkMode2 {
		public WorkMode2Simple(ClimaticSystemWorkMode climaticSystemWorkmode, bool slaveRegulatorOnByMaster) {
			ClimaticSystemWorkmode = climaticSystemWorkmode;
			SlaveRegulatorOnByMaster = slaveRegulatorOnByMaster;
		}

		public ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		public bool SlaveRegulatorOnByMaster { get; }
	}
}