using System;
using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SensorIndications {
	public abstract class SensorIndicationBytesPairCheck<T> : ISensorIndication<T> {
		
		private readonly BytesPair _noLinkCode;
		private readonly BytesPair _value;
		private T _indication;

		protected SensorIndicationBytesPairCheck(BytesPair value, BytesPair noLinkCode) {
			_value = value;
			_noLinkCode = noLinkCode;
		}

		public bool NoLinkWithSensor => _value == _noLinkCode;

		public T Indication {
			get {
				if (NoLinkWithSensor)
					throw new Exception(SensorIndicationExt.NoLinkText);
				return _indication;
			}
			set { _indication = value; }
		}

		protected abstract T GetIndiction();

		public string ToString(Func<T, string> formatter) {
			if (NoLinkWithSensor)
				return SensorIndicationExt.NoLinkText;
			return formatter(_indication);
		}
	}
}