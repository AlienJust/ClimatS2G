using ParamCentric.Common.Contracts;

namespace ParamCentric.UserInterface.Contracts {
	public interface ISettableByUserParameter<T> : IParameter {
		T FormattedValue { get; set; }
	}
}
