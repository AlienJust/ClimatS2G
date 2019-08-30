﻿using System.Collections.Generic;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface ISharedMainViewModel
    {
        IThreadNotifier Notifier { get; }
        IWindowSystem WindowsSystem { get; }
        ILogger Logger { get; }

        FrameworkElement TopContent { get; set; }
        void AddTab(TabItemViewModel tabVm);

        ComPortControlViewModel AddChannel(string channelName);

        Dictionary<string, IParameterViewModel> Parameters { get; }

        void AddParameter(string key, IParameterDescription description, IPsnProtocolParameterConfigurationVariable configuration);
    }
}