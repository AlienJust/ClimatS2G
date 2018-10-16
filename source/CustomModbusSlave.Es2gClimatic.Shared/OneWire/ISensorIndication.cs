using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire {
	public interface ISensorIndication<T> {
		bool NoLinkWithSensor { get; }
		T Indication { get; }

		string ToString(Func<T, string> formatter);
	}
}
