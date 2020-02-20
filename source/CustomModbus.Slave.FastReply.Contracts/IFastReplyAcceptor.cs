namespace CustomModbus.Slave.FastReply.Contracts {
	public interface IFastReplyAcceptor {
		void AcceptReply(byte[] replyBytes);
	}
}
