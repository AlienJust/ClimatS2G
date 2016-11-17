namespace CustomModbus.Slave.FastReply.Contracts {
	public interface IFastReplyGenerator {
		byte[] GenerateReply();
	}
}