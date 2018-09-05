using System;
using System.Collections.Generic;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	internal sealed class StdNotifier : IStdNotifier {
		private readonly List<ICmdListenerStd> _listeners;
		private readonly object _listenersSyncObj;

		private readonly object _channelsSyncObj;
		private readonly List<ISerialChannel> _channels;

		public StdNotifier() {
			_listenersSyncObj = new object();
			_listeners = new List<ICmdListenerStd>();

			_channelsSyncObj = new object();
			_channels = new List<ISerialChannel>();
		}

		private void ChannelOnCommandHeared(ICommandPart commandPart) {
			lock (_listenersSyncObj) {
				//Console.WriteLine("Подслушана команда " + commandPart.CommandCode);
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

		public void AddSerialChannel(ISerialChannel channel) {
			lock (_channelsSyncObj) {
				_channels.Add(channel);
				channel.CommandHeared += ChannelOnCommandHeared;

				Console.WriteLine("Канал зарегистрирован, итого каналов в списке: " + _channels.Count);
			}
		}

		public void RemoveSerialChannel(ISerialChannel channel) {
			lock (_channelsSyncObj) {
				if (_channels.Contains(channel)) {
					_channels.Remove(channel);
					channel.CommandHeared -= ChannelOnCommandHeared; // TODO: maybe subscribe and unsubscribe event in own thread?
				}
			}
		}
	}
}
