using ParamCentric.Common.Contracts;

namespace ParamCentric.UserInterface.Contracts
{
    public interface IDisplayableParameter<out T> : IParameter
    {
        T ReceivedValueFormatted { get; }
    }
}