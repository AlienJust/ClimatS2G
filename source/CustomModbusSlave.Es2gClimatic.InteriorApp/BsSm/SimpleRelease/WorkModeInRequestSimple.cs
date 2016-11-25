using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class WorkModeInRequestSimple : IWorkModeInRequest {
		public WorkModeInRequestSimple(bool isHeater3kWOn, bool inquiryComprtessorOn, bool heating, bool cooling, bool station, bool tunnel, bool longDistanceJourney, bool reserve) {
			IsHeater3kWOn = isHeater3kWOn;
			InquiryComprtessorOn = inquiryComprtessorOn;
			Heating = heating;
			Cooling = cooling;
			Station = station;
			Tunnel = tunnel;
			LongDistanceJourney = longDistanceJourney;
			Reserve = reserve;
		}

		public bool IsHeater3kWOn { get; }
		public bool InquiryComprtessorOn { get; }
		public bool Heating { get; }
		public bool Cooling { get; }
		public bool Station { get; }
		public bool Tunnel { get; }
		public bool LongDistanceJourney { get; }
		public bool Reserve { get; }
	}
}