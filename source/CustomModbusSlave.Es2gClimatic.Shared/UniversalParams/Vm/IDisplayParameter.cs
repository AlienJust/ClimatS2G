using System.Threading;

namespace ParamControls.Vm {
	public interface IDisplayParameter<out T> : IDisplayParameter {
		T DisplayValue { get; }
		bool IsValueFallbackOrUnknown { get; }
		event DisplayParameterValueMaybeChangedDelegate DisplayParameterValueMaybeChanged;
	}

	public delegate void DisplayParameterValueMaybeChangedDelegate();

	public interface IDisplayParameter {
		string DisplayName { get; }

	/// <summary>
	/// Chart parameters - used for charts to qualify as unique name, also 
	/// Groups and parameters - used for searching
	/// </summary>
		string FullName { get; }
	}
}