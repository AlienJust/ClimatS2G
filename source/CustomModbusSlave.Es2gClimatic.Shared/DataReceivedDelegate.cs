using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public delegate void DataReceivedDelegate<in T>(IList<byte> bytes, T data);
}
