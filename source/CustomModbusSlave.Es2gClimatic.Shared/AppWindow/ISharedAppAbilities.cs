using AlienJust.Support.Loggers;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
	/// <summary>
	/// Common abilities of application, available to content
	/// </summary>
	public interface ISharedAppAbilities {
		AppVersion Version { get; }

		IFastReplyGenerator ReplyGenerator { get; }
		IFastReplyAcceptor ReplyAcceptor { get; }
		IParameterSetter ParamSetter { get; }

		ModbusRtuParamReceiver RtuParamReceiver { get; }
		string TestPortName { get; }
		RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }

		SerialChannel SerialChannel { get; }
		CommandHearedTimerNotThreadSafe CommandHearedTimeoutMonitor { get; }
	}
}