namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public interface IDisplayAndSettableParameter<out TDisplay, TSet> : IDisplayParameter<TDisplay>, ISettParameter<TSet> {
		
	}
}