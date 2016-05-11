namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal interface IWorkMode {
		bool IsHeater3kWOn { get; }
		bool InquiryComprtessorOn { get; }
		bool Heating { get; }
		bool Cooling { get; }
		bool Station { get; }
		bool Tunnel { get; }
		bool LongDistanceJourney { get; }
		bool Reserve { get; }
	}
}