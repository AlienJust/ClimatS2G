namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16 {
	class MukCondenserMukCondenserRequest16DataSimple : IMukCondenserRequest16Data {
		public IMukCondenserWorkmodeCommandFromKsm MukCondenserWorkmodeCommandFromKsm { get; set; }
		public int PressureSetting { get; set; }
		public int Reserve14 { get; set; }
	}
}
