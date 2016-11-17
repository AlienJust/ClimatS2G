using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class WorkModeReplySimple : IWorkModeReply {
		public WorkModeReplySimple(bool relabilityFlag, bool powerLimitation, bool station, bool tunnel, bool longDistanceJourney, bool hasVoltage3000V, bool hasVoltage380V, bool isCompressorSwitchOnPermitted) {
			RelabilityFlag = relabilityFlag;
			PowerLimitation = powerLimitation;
			Station = station;
			Tunnel = tunnel;
			LongDistanceJourney = longDistanceJourney;
			HasVoltage3000V = hasVoltage3000V;
			HasVoltage380V = hasVoltage380V;
			IsCompressorSwitchOnPermitted = isCompressorSwitchOnPermitted;
		}

		public bool RelabilityFlag { get; }
		public bool PowerLimitation { get; }
		public bool Station { get; }
		public bool Tunnel { get; }
		public bool LongDistanceJourney { get; }
		public bool HasVoltage3000V { get; }
		public bool HasVoltage380V { get; }
		public bool IsCompressorSwitchOnPermitted { get; }
	}
}