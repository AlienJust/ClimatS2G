using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterViewModelSimple : ViewModelBase, IParameterViewModel
    {
        public string Name { get; }

        public IParameterGetterViewModel Getter { get; }

        public IParameterSetterViewModel Setter { get; }

        public ParameterViewModelSimple(string customName, string parameterNameFromConfiguration, IParameterGetterViewModel getter, IParameterSetterViewModel setter)
        {
            Name = customName ?? parameterNameFromConfiguration;
            Getter = getter;
            Setter = setter;
        }
    }
}