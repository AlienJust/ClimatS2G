namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	public class WorkModeReplyBuilderFromByte : IBuilder<IWorkMode> {
		private readonly byte _absoluteValue;

		public WorkModeReplyBuilderFromByte(byte absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IWorkMode Build() {
			return new WorkModeSimple(
				(_absoluteValue & 0x01) == 0x01, // zb bit 0
				(_absoluteValue & 0x02) == 0x02, // zb bit 1
				(_absoluteValue & 0x04) == 0x04, // zb bit 2
				(_absoluteValue & 0x08) == 0x08, // zb bit 3

				(_absoluteValue & 0x10) == 0x10, // zb bit 4
				(_absoluteValue & 0x20) == 0x20, // zb bit 5
				(_absoluteValue & 0x40) == 0x40, // zb bit 6
				(_absoluteValue & 0x80) == 0x80 //  zb bit 7
				);
		}
	}
}