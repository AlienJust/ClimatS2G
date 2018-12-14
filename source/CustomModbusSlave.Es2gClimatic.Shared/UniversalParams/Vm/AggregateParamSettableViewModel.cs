using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
	/// <typeparam name="TDisplaySet">Settable by user data type</typeparam>
	public sealed class AggregateParamSettableViewModel<TDisplaySet> : ViewModelBase, IAggregateParameterViewModel {
		private readonly string _uniqNamePrefix;
		private readonly ISettParameter<TDisplaySet> _parameter;

		public IList<IDisplayParameter> DisplayParameters { get; }
		public string DisplayName => _uniqNamePrefix + ": " + _parameter.DisplayName;

		public AggregateParamSettableViewModel(ISettParameter<TDisplaySet> parameter, params string[] uniqNamePrefix) {
			_uniqNamePrefix = string.Join(", ", uniqNamePrefix);
			_parameter = parameter;
			DisplayParameters = new List<IDisplayParameter> {_parameter};
		}
	}
}