using System;
using System.Collections.Generic;
using System.IO;
//using AlienJust.Adaptation.ConsoleLogger;
//using AlienJust.Support.Loggers;
//using AlienJust.Support.Loggers.Contracts;
//using AlienJust.Support.Text;
//using AlienJust.Support.Text.Contracts;
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

            RtuParamReceiver = new ModbusRtuParamReceiver();

            CmdNotifierStd = new StdNotifier();
            CmdNotifierStd.AddListener(RtuParamReceiver);
            _channels = new Dictionary<string, SerialChannelWithTimeoutMonitorAndSendReplyAbility>();

            var paramLoggerAndRegPoint = new ParamLoggerRegistrationPointThreadSafe();
            ParameterLogger = paramLoggerAndRegPoint;
            ParamLoggerRegistrationPoint = paramLoggerAndRegPoint;
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
    }
}