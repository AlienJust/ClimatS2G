using CustomModbusSlave.Es2gClimatic.BsSm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class WorkMode2Simple : IWorkMode2 {
		public WorkMode2Simple(ClimaticSystemWorkMode climaticSystemWorkmode, bool slaveRegulatorOnByMaster) {
			ClimaticSystemWorkmode = climaticSystemWorkmode;
			SlaveRegulatorOnByMaster = slaveRegulatorOnByMaster;
		}

		public ClimaticSystemWorkMode ClimaticSystemWorkmode { get; }
		public bool SlaveRegulatorOnByMaster { get; }
	}
}