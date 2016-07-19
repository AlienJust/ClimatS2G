namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm {
	public interface IFastReplyAcceptor {
		void AcceptReply(byte[] replyBytes);
	}
}