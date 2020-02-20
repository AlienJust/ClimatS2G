using System;

namespace CustomModbusSlave.Contracts {
	public interface ISerialPortContainer : ISendAbility {
		byte[] Read(int count, TimeSpan timeout);
		void Close();
		void Open();
		bool IsOpen { get; }
	}
}
