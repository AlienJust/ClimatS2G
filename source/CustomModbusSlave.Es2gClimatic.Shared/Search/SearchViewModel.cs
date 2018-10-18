using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using ParamControls.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.Search {
	public class SearchViewModel : ViewModelBase {
		private string _searchText;
		public readonly List<IDisplayParameter> _globalParamList;
		public GroupParamViewModel ResultParamsVm { get; }
		public SearchViewModel() {
			_globalParamList = new List<IDisplayParameter>();
			ResultParamsVm = new GroupParamViewModel("Результаты поиска", null);
		}

		public void RegisterParam(IDisplayParameter param) {
			if (param != null) {
				_globalParamList.Add(param);
			}
		}

		public string SearchText {
			get => _searchText;
			set {
				if (value != _searchText) {
					_searchText = value;
					RaisePropertyChanged(()=>SearchText);
					UpdateParamsVm();
				}
			}
		}

		private void UpdateParamsVm() {
			ResultParamsVm.GroupItems.Clear();
			foreach (var parameter in _globalParamList) {
				if (parameter.DisplayName.Contains(_searchText)) {
					ResultParamsVm.GroupItems.Add(parameter);
				}
			}
		}
	}
}