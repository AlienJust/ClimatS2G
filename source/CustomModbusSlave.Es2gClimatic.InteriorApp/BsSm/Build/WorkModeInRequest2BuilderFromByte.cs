using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Build {
	class WorkModeInRequest2BuilderFromByte : IBuilder<IWorkModeInRequest2> {
		private readonly byte _absoluteValue;

		public WorkModeInRequest2BuilderFromByte(byte absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IWorkModeInRequest2 Build() {
			return new WorkModeInRequest2Simple(new ClimaticSystemWorkModeBuilderFromInt(_absoluteValue & 0x0F).Build(), 
				(_absoluteValue & 0x10) == 0x10);
		}
	}
}
