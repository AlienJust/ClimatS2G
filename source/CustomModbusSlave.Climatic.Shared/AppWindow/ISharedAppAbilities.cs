using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DrillingRig.ConfigApp.AppControl.ParamLogger;
using System;
using System.Collections.Generic;

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
        Dictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>> PsnProtocolConfigurationParams { get; }
        IParametersPresenter GetParametersPresentation(string filename);

        IParamListener ParamListener { get; }

        ICommandPartListener CommandPartListener { get; }
    }
}