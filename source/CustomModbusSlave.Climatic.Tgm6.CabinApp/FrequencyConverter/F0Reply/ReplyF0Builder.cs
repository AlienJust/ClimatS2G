using System;
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Reply
{
    public class ReplyF0Builder : IBuilder<IReplyF0>
    {
        private readonly IList<byte> _bytes;

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
}