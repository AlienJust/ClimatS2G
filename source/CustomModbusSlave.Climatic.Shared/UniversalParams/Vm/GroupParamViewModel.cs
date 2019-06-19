using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public sealed class GroupParamViewModel : ViewModelBase, IDisplayGroup
    {
        public string DisplayName { get; }
        private bool _isGroupExpanded;
        public ObservableCollection<IDisplayParameter> ParametersAndGroups { get; }

        public GroupParamViewModel(string displayName)
        {
            DisplayName = displayName;
            _isGroupExpanded = true;
            ParametersAndGroups = new ObservableCollection<IDisplayParameter>();
        }

        public GroupParamViewModel(string displayName, IEnumerable<IDisplayParameter> displayParameters) : this(displayName)
        {
            if (displayParameters == null) return;
            foreach (var parameter in displayParameters)
            {
                ParametersAndGroups.Add(parameter);
            }
        }

        public bool IsGroupExpanded
        {
            get => _isGroupExpanded;
            set => SetProp(() => _isGroupExpanded != value, () => _isGroupExpanded = value, () => IsGroupExpanded);
        }

        public void AddParameterOrGroup(IDisplayParameter parameter)
        {
            ParametersAndGroups.Add(parameter);
        }
    }
}