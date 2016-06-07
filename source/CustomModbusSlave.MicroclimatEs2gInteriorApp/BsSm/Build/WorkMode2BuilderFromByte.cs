using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class WorkMode2BuilderFromByte : IBuilder<IWorkMode2> {
		private readonly byte _absoluteValue;

		public WorkMode2BuilderFromByte(byte absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IWorkMode2 Build() {
			return new WorkMode2Simple(new ClimaticSystemWorkModeBuilderFromInt((_absoluteValue & 0x0F)).Build(), 
				(_absoluteValue & 0x10) == 0x10);
		}
	}
}