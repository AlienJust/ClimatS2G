using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.CommandHearedTimer.Contracts {
	internal interface ICommandHearedTimer {
		DateTime LastTimeAnyCommandWasHeared { get; set; }
		event SimplestDelegateEver NoAnyCommandWasHearedTooLong;
		event SimplestDelegateEver SomeCommandWasHeared;
	}
}
