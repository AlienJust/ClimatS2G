//using AlienJust.Support.Loggers;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    /// <summary>
    /// Common abilities of application, available to content
    /// </summary>
    public interface ISharedAppAbilities
    {
        AppVersion Version { get; }
        string TestPortName { get; }
        bool IsHourCountersVisible { get; }

        //RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }

        SerialChannelWithTimeoutMonitorAndSendReplyAbility CreateChannel(string channelName);
        void DestroyChannel(string channelName);

        /// <summary>
        /// To notify std listeners
        /// </summary>
        IStdNotifier CmdNotifierStd { get; }

        ModbusRtuParamReceiver RtuParamReceiver { get; }

        IParamLoggerRegistrationPoint ParamLoggerRegistrationPoint { get; }
        IParameterLogger ParameterLogger { get; }
        IPsnProtocolConfiguration PsnProtocolConfiguration { get; }
    }
}