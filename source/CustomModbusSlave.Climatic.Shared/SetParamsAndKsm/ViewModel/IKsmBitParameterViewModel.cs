using ParamCentric.Modbus.Contracts;
using ParamCentric.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel
{
    public interface IKsmBitParameterViewModel : IReceivableParameter, IDisplayableParameter<bool?> { }
}