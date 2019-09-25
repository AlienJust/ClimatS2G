using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class CommandPartAndParamListenerSimple : IParamListener, ICommandPartListener
    {
        private readonly IStdNotifier _stdNotifier;

        public event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;
        public event EventHandler<CommandPartReceivedEventArgs> CommandPartReceived;

        public CommandPartAndParamListenerSimple(IStdNotifier stdNotifier)
        {
            _stdNotifier = stdNotifier;
        }

        public void AddPsnCommandPartConfigurationToListen(IPsnProtocolCommandPartConfiguration psnCommandPartConfiguration)
        {
            var listener = new CmdListenerCmdPartAndBytes(psnCommandPartConfiguration);
            listener.DataReceived += ListenerDataReceived;

            _stdNotifier.AddListener(listener);
        }

        private void ListenerDataReceived(IList<byte> bytes, ICmdPartConfigAndBytes data)
        {
            var commandPartReceivedEvent = CommandPartReceived;
            commandPartReceivedEvent?.Invoke(this, new CommandPartReceivedEventArgs(data));

            foreach (var param in data.CmdPartConfig.VarParams)
            {
                try
                {
                    RaiseValueReceived(param.Id.IdentyString, param.GetValue(data.DataBytes, 0));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void RaiseValueReceived(string id, double value)
        {
            var valueReceivedEvent = ValueReceived;
            valueReceivedEvent?.Invoke(this, new ParameterValueReceivedEventArgs(id, value));
        }
    }
}