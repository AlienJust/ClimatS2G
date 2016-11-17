namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.Request16 {
	class Request16Data : IRequest16Data {
		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; set; }
		public int PressureSetting { get; set; }
		public int Reserve14 { get; set; }
	}
}