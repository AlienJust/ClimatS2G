using System;
using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.Search {
	public class SearchViewModel : ViewModelBase {
		private string _filterText;
		public readonly List<IDisplayGroup> _rootGroups;
		public GroupParamViewModel ResultParamsVm { get; }

		public SearchViewModel() {
			_rootGroups = new List<IDisplayGroup>();
			ResultParamsVm = new GroupParamViewModel("Результаты поиска");
		}

		public void RegisterTopLevelGroup(IDisplayGroup rootGroup) {
			if (rootGroup != null) {
				_rootGroups.Add(rootGroup);
				UpdateParamsVm();
			}
		}

		public string FilterText {
			get => _filterText;
			set {
				if (value != _filterText) {
					_filterText = value;
					RaisePropertyChanged(() => FilterText);
					UpdateParamsVm();
				}
			}
		}

		private void UpdateParamsVm() {
			ResultParamsVm.ParametersAndGroups.Clear();
			// if no filter text - adding all groups:
			if (string.IsNullOrEmpty(_filterText)) {
				foreach (var topLevelGroup in _rootGroups) {
					ResultParamsVm.ParametersAndGroups.Add(topLevelGroup);
				}
			}
			else {
				foreach (var topLevelGroup in _rootGroups) {
					int paramsCount = 0;
					var copyOfDispGroup = FilterGroupsAndParamsRecurse(topLevelGroup, new List<string>(), ref paramsCount);
					if (copyOfDispGroup != null) ResultParamsVm.AddParameterOrGroup(copyOfDispGroup);
				}
				
				
			}
		}

		private IDisplayGroup FilterGroupsAndParamsRecurse(IDisplayGroup dispGroup, IEnumerable<string> parentNames, ref int paramsCount) {
			List<string> searchTags = new List<string>(parentNames) {dispGroup.DisplayName};

			int subParamsCount = paramsCount;
			var copyOfDispGroup = new GroupParamViewModel(dispGroup.DisplayName);
			foreach (var param in dispGroup.ParametersAndGroups) {
				if (param is IDisplayGroup group) {
					var copyOfSubgroup = FilterGroupsAndParamsRecurse(group, searchTags, ref paramsCount);
					if (copyOfSubgroup != null) {
						copyOfDispGroup.AddParameterOrGroup(copyOfSubgroup);
					}
				}
				else {
					if (searchTags.Any(tag => tag.IndexOf(_filterText, StringComparison.OrdinalIgnoreCase) >= 0) || param.DisplayName.IndexOf(_filterText, StringComparison.OrdinalIgnoreCase) >= 0) {
						copyOfDispGroup.AddParameterOrGroup(param);
						paramsCount++;
					}
				}
			}

			if (subParamsCount == paramsCount) return null; // noting was found inside
			return copyOfDispGroup;
		}
	}
}