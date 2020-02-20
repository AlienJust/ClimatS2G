using System.ComponentModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public interface IDisplayParameter<out T> : IDisplayParameter, INotifyPropertyChanged
    {
        T DisplayValue { get; }

        bool IsValueFallbackOrUnknown { get; }
        //event DisplayParameterValueMaybeChangedDelegate DisplayParameterValueMaybeChanged;
    }

    //public delegate void DisplayParameterValueMaybeChangedDelegate();

    public interface IDisplayParameter
    {
        /// <summary>
        /// Chart parameters - used for charts to qualify as unique name 
        /// Groups and parameters - used for displaying and searching
        /// </summary>
        string DisplayName { get; }
    }
}