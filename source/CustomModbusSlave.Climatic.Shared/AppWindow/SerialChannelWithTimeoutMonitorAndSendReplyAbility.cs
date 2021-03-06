﻿using System;
using System.Linq;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class SerialChannelWithTimeoutMonitorAndSendReplyAbility
    {
        public ISerialChannel Channel { get; }
        public CommandHearedTimerNotThreadSafe TimeoutMonitor { get; }
        public IFastReplyGenerator ReplyGenerator { get; }
        public IFastReplyAcceptor ReplyAcceptor { get; }
        public IParameterSetter ParamSetter { get; }

        public SerialChannelWithTimeoutMonitorAndSendReplyAbility(ISerialChannel channel)
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

        private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendability)
        {
            if (commandPart.Address == 20)
            {
                if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Length == 8)
                {
                    ReplyAcceptor.AcceptReply(commandPart.ReplyBytes);
                    var reply = ReplyGenerator.GenerateReply();
                    sendability.Send(reply);
                    //Console.WriteLine("Sended reply");
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