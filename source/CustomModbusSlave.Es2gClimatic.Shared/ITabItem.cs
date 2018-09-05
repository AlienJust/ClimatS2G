﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public interface ITabItem {
		string ShortHeader { get; }
		string FullHeader { get; }
		FrameworkElement Content { get; }
		void OnWindowClose();
	}
}