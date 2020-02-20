using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer.Contracts {
	internal interface ICommandHearedTimer {
		DateTime LastTimeAnyCommandWasHeared { get; set; }
		event SimplestDelegateEver NoAnyCommandWasHearedTooLong;
		event SimplestDelegateEver SomeCommandWasHeared;
	}
}
