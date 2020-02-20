using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class WorkModeInRequest2Simple : IWorkModeInRequest2 {
		public WorkModeInRequest2Simple(ClimaticSystemWorkMode climaticSystemWorkmode, bool slaveRegulatorOnByMaster) {
			ClimaticSystemWorkmode = climaticSystemWorkmode;
			SlaveRegulatorOnByMaster = slaveRegulatorOnByMaster;
		}

		public ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		public bool SlaveRegulatorOnByMaster { get; }
	}
}
