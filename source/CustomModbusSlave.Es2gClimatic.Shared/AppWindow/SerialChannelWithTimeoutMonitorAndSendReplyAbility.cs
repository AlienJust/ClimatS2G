using System;
using System.Linq;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
	public sealed class SerialChannelWithTimeoutMonitorAndSendReplyAbility
	{
		public SerialChannel Channel { get; }
		public CommandHearedTimerNotThreadSafe TimeoutMonitor { get; }
		public IFastReplyGenerator ReplyGenerator { get; }
		public IFastReplyAcceptor ReplyAcceptor { get; }
		public IParameterSetter ParamSetter { get; }

		public SerialChannelWithTimeoutMonitorAndSendReplyAbility(SerialChannel channel)
		{
			Channel = channel;
			Channel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			TimeoutMonitor = new CommandHearedTimerNotThreadSafe(Channel, TimeSpan.FromSeconds(1));
			TimeoutMonitor.Start();

			var replyGenerator = new ReplyGeneratorWithQueueAttempted();
			ReplyGenerator = replyGenerator;
			ReplyAcceptor = replyGenerator;
			ParamSetter = replyGenerator;
		}

		private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendability) {
			if (commandPart.Address == 20) {
				if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Count == 8) {
					ReplyAcceptor.AcceptReply(commandPart.ReplyBytes.ToArray()); // TODO: bad performance cause of .ToArray() call
					var reply = ReplyGenerator.GenerateReply();
					sendability.Send(reply);
				}
			}
		}

		public void BecameUnused()
		{
			Channel.CommandHearedWithReplyPossibility -= SerialChannelOnCommandHearedWithReplyPossibility;
			TimeoutMonitor.Stop();
		}
	}
}
