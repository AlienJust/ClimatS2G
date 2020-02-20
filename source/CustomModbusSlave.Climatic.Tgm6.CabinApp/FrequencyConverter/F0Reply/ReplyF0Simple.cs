namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Reply
{
    public class ReplyF0Simple : IReplyF0
    {
        public ReplyF0Simple(IFcState fcState)
        {
            FcState = fcState;
        }
        public IFcState FcState { get; }
    }
}