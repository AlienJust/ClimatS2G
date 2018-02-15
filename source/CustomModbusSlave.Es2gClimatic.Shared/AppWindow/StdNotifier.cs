using System;
using System.Collections.Generic;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
	internal sealed class StdNotifier : IStdNotifier {
		private readonly List<ICmdListenerStd> _listeners;
		private readonly object _listenersSyncObj;
		public StdNotifier(SerialChannel channel) {
			_listenersSyncObj = new object();
			_listeners = new List<ICmdListenerStd>();

			channel.CommandHeared += ChannelOnCommandHeared;
		}

		private void ChannelOnCommandHeared(ICommandPart commandPart) {
			lock (_listenersSyncObj) {
				foreach (var cmdListenerStd in _listeners) {
					cmdListenerStd.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
				}
			}
		}

		public void AddListener(ICmdListenerStd listener) {
			lock (_listenersSyncObj) {
				_listeners.Add(listener);
			}
		}
	}
}