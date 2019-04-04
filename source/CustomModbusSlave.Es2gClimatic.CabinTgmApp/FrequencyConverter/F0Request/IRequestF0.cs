using System;
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter
{
    public interface IReplyF0
    {
        IFcState FcState { get; }
    }

    public interface IFcState
    {
        bool OverloadByCurrent250 { get; }
        bool ProtectionByTemperature { get; }
        bool TransistorsPowerVoltageTooHighOrToLow { get; }
        bool ShortCircuitOfTheBottomTransistors { get; }
        bool TooHighInputVoltage { get; }
        bool OverloadByCurrent150 { get; }
    }
    
    public class CmdListenerFcF0Request : CmdListenerBase<IReplyF0>
    {
        public CmdListenerFcF0Request(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IReplyF0 BuildData(IList<byte> bytes)
        {
            return new ReplyF0Builder(bytes).Build();
        }
    }

    public class ReplyF0Builder : IBuilder<IReplyF0>
    {
        private IList<byte> _bytes;

        public ReplyF0Builder(IList<byte> bytes)
        {
            _bytes = bytes;
        }

        public IReplyF0 Build()
        {
            throw new NotImplementedException();
            //return new ReplyF0Simple(IFcState IFcState);
        }
    }

    public class ReplyF0Simple : IReplyF0 {
        public ReplyF0Simple(IFcState fcState)
        {
            FcState = fcState;
        }
        public IFcState FcState { get; }
    }
}