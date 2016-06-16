namespace CustomModbusSlave.MicroclimatEs2gApp {
	interface IFastReplyAcceptor {
		void AcceptReply(byte[] replyBytes);
	}
}