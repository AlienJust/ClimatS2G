using System;
using System.Collections.Generic;
using System.IO;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class SharedAppAbilities : ISharedAppAbilities
    {
        public AppVersion Version { get; }
        public bool IsHourCountersVisible { get; }

        private readonly Dictionary<string, SerialChannelWithTimeoutMonitorAndSendReplyAbility> _channels;
        public string TestPortName => "ТЕСТ";
        public IStdNotifier CmdNotifierStd { get; }
        public Dictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>> PsnProtocolConfigurationParams { get; }
        public ModbusRtuParamReceiver RtuParamReceiver { get; }

        public IParamLoggerRegistrationPoint ParamLoggerRegistrationPoint { get; }
        public IParameterLogger ParameterLogger { get; }

        public IPsnProtocolConfiguration PsnProtocolConfiguration { get; }

        public SharedAppAbilities(string psnProtocolFileName)
        {
            var isFullVersion = File.Exists("FullVersion.txt");
            var isHalfOrFullVersion = isFullVersion;
            if (!isHalfOrFullVersion) isHalfOrFullVersion = File.Exists("HalfVersion.txt");

            Version = isFullVersion ? AppVersion.Full :
                isHalfOrFullVersion ? AppVersion.Half : AppVersion.Base;

            IsHourCountersVisible = File.Exists("HourCounters.txt");

            PsnProtocolConfiguration = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, psnProtocolFileName)).LoadConfiguration();

            var allPsnParams = new Dictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>>();
            foreach (var psnCommandPart in PsnProtocolConfiguration.CommandParts)
            {
                foreach(var param in psnCommandPart.VarParams)
                {
                    allPsnParams.Add(param.Id.IdentyString, new Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>(psnCommandPart, param));
                }
            }

            PsnProtocolConfigurationParams = allPsnParams;
            RtuParamReceiver = new ModbusRtuParamReceiver();

            CmdNotifierStd = new StdNotifier();
            CmdNotifierStd.AddListener(RtuParamReceiver);
            _channels = new Dictionary<string, SerialChannelWithTimeoutMonitorAndSendReplyAbility>();


            var paramLoggerAndRegPoint = new ParamLoggerRegistrationPointThreadSafe();
            ParameterLogger = paramLoggerAndRegPoint;
            ParamLoggerRegistrationPoint = paramLoggerAndRegPoint;

            var paramListener = new CommandPartAndParamListenerSimple(CmdNotifierStd);
            
            foreach (var cmdPart in PsnProtocolConfiguration.CommandParts)
            {
                paramListener.AddPsnCommandPartConfigurationToListen(cmdPart);
            }

            ParamListener = paramListener;
            CommandPartListener = paramListener;
        }

        public SerialChannelWithTimeoutMonitorAndSendReplyAbility CreateChannel(string channelName)
        {
            var serialChannel = new SerialChannelWithTimeoutMonitorAndSendReplyAbility(new SerialChannel(new CommandPartSearcherPsnConfigBasedFast(PsnProtocolConfiguration)));
            _channels.Add(channelName, serialChannel);
            CmdNotifierStd.AddSerialChannel(serialChannel.Channel);
            return serialChannel;
        }

        public void DestroyChannel(string channelName)
        {
            if (_channels.ContainsKey(channelName))
            {
                var channel = _channels[channelName];
                _channels.Remove(channelName);
                CmdNotifierStd.RemoveSerialChannel(channel.Channel);
                channel.BecameUnused();
            }
        }

        public IParametersPresenter GetParametersPresentation(string filename)
        {
            return ParametersPresenterXmlBuilder.BuildParametersPresentationFromXml(filename);
        }
             
        public IParamListener ParamListener { get; }

        public ICommandPartListener CommandPartListener { get; }
    }
}