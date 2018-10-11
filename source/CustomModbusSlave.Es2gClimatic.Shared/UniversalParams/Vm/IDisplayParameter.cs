namespace ParamControls.Vm {
	public interface IDisplayParameter<out T> : IDisplayParameter {
		T DisplayValue { get; }
	}

	public interface IDisplayParameter {
		string DisplayName { get; }
	}
}