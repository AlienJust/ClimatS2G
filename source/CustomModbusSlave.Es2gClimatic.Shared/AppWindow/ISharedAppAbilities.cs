using AlienJust.Support.Loggers;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	/// <summary>
	/// Common abilities of application, available to content
	/// </summary>
	public interface ISharedAppAbilities {
		AppVersion Version { get; }
		string TestPortName { get; }
		RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }
		
		SerialChannelWithTimeoutMonitorAndSendReplyAbility CreateChannel(string channelName);
		void DestroyChannel(string channelName);

		/// <summary>
		/// To notify std listeners
		/// </summary>
		IStdNotifier CmdNotifierStd { get; }

		ModbusRtuParamReceiver RtuParamReceiver { get; }
	}
}