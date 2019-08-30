using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParamListener
    {
        event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;

        //void AddPsnCommandPartConfigurationToListen(IPsnProtocolCommandPartConfiguration psnCommandPartConfiguration);

    }

    public class ParameterValueReceivedEventArgs : EventArgs
    {
        public string ParameterId { get; private set; }

        public double Value { get; private set; }

        public ParameterValueReceivedEventArgs(string parameterId, double value) : base()
        {
            ParameterId = parameterId;
            Value = value;
        }
    }

    internal sealed class ParamListenerSimple : IParamListener
    {
        private readonly IStdNotifier _stdNotifier;

        public event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;

        public ParamListenerSimple(IStdNotifier stdNotifier)
        {
            _stdNotifier = stdNotifier;
        }

        public void AddPsnCommandPartConfigurationToListen(IPsnProtocolCommandPartConfiguration psnCommandPartConfiguration)
        {
            var listener = new CmdListenerCmdPartAndBytes(psnCommandPartConfiguration);
            //listeners.Add(new Tuple<IPsnProtocolCommandPartConfiguration, ICmdListener<ICmdPartConfigAndBytes>>(cmdPart, listener));
            //appAbilities.CmdNotifierStd.AddListener(listener);

            listener.DataReceived += ListenerDataReceived;

            _stdNotifier.AddListener(listener);
        }

        private void ListenerDataReceived(IList<byte> bytes, ICmdPartConfigAndBytes data)
        {
            foreach (var param in data.CmdPartConfig.VarParams)
            {
                RaiseValueReceived(param.Id.IdentyString, param.GetValue(data.DataBytes, 0));
            }
        }

        private void RaiseValueReceived(string id, double value)
        {
            var valueReceivedEvent = ValueReceived;
            valueReceivedEvent?.Invoke(this, new ParameterValueReceivedEventArgs(id, value));
        }
    }
}