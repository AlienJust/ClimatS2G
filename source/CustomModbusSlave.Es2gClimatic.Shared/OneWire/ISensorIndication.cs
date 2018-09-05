using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.SensorIndications {
	public interface ISensorIndication<out T> {
		bool NoLinkWithSensor { get; }
		T Indication { get; }

		string ToString(Func<T, string> formatter);
	}
}
