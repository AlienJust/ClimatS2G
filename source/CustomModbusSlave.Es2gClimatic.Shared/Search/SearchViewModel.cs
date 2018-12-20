using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.Search {
	public class SearchViewModel : ViewModelBase {
		private readonly IThreadNotifier _uiNotifier;
		private string _filterText;
		public readonly List<IDisplayGroup> _rootGroups;
		public GroupParamViewModel ResultParamsVm { get; }

		private readonly IWorker<Action> _searchWorker;

		public SearchViewModel(IThreadNotifier uiNotifier) {
			_uiNotifier = uiNotifier;
			_rootGroups = new List<IDisplayGroup>();
			ResultParamsVm = new GroupParamViewModel("Результаты поиска");
			_searchWorker = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("SearchTw", a=>a(), ThreadPriority.BelowNormal, true, null);
			
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
			var filterText = _filterText;
			
			if (string.IsNullOrEmpty(filterText)) {
				foreach (var topLevelGroup in _rootGroups) {
					ResultParamsVm.ParametersAndGroups.Add(topLevelGroup);
				}
			}
			else {
				_searchWorker.AddWork(() => {
					foreach (var topLevelGroup in _rootGroups) {
						int paramsCount = 0;
						var copyOfDispGroup = FilterGroupsAndParamsRecurse(filterText, topLevelGroup, new List<string>(), ref paramsCount);
						if (copyOfDispGroup != null) {
							_uiNotifier.Notify(() => {
								ResultParamsVm.AddParameterOrGroup(copyOfDispGroup);
							});
						}
					}
				});
			}
		}

		private IDisplayGroup FilterGroupsAndParamsRecurse(string filterText, IDisplayGroup dispGroup, IEnumerable<string> parentNames, ref int paramsCount) {
			List<string> searchTags = new List<string>(parentNames) {dispGroup.DisplayName};

			int subParamsCount = paramsCount;
			var copyOfDispGroup = new GroupParamViewModel(dispGroup.DisplayName);
			foreach (var param in dispGroup.ParametersAndGroups) {
				if (param is IDisplayGroup group) {
					var copyOfSubgroup = FilterGroupsAndParamsRecurse(filterText, group, searchTags, ref paramsCount);
					if (copyOfSubgroup != null) {
						copyOfDispGroup.AddParameterOrGroup(copyOfSubgroup);
					}
				}
				else {
					if (searchTags.Any(tag => tag.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0) || param.DisplayName.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0) {
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