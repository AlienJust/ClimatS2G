using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	internal sealed class SdTabInterfaceBuilder : IBuilderManyToOne<IDisplayGroup> {
		private readonly IThreadNotifier _uiNotifier;

		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;
		//private readonly SearchViewModel _svm;

		private readonly IParameterLogger _parameterLogger;
		private readonly IParameterSetter _parameterSetter;
		private readonly AppVersion _version;

		public SdTabInterfaceBuilder(IThreadNotifier mainWindowUiUiNotifier, ICmdListener<IList<BytesPair>> cmdListenerKsmParams, IParameterLogger parameterLogger, IParameterSetter parameterSetter, AppVersion version) {
			_uiNotifier = mainWindowUiUiNotifier;
			_cmdListenerKsmParams = cmdListenerKsmParams;

			_parameterLogger = parameterLogger;
			_parameterSetter = parameterSetter;
			_version = version;
		}

		public IDisplayGroup Build() {
			var timeCountersGroup = new GroupParamViewModel("Моточасы: ");
			//const string reply03GroupName = "МУК 8, заслонка зима лето";
			


			// setting group of settable params
			//var setParamsGroup = new GroupParamViewModel("Параметры КСМ");
			
			var recvParam39 = new RecvParam<int, IList<BytesPair>>("39: Обеззараживатель, почасовой счетчик работы", _cmdListenerKsmParams, data=>data[39].LowFirstUnsignedValue);
			var setParam39 = new SettParamViewModel<int>(recvParam39.ReceiveName, _uiNotifier, val => {
				if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
			}, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(39, (ushort)val, callback));
			
			var dispsetParameter39 = new DispParamSettableViewModel<int, int, int>(recvParam39.ReceiveName, recvParam39, _uiNotifier, i => i, 0, 0, val => {
				if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
			}, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(39, (ushort)val, callback));


			// defining version of app, if full then using checkboxVm, else without checkbox:
			IDisplayParameter setParam39Vm = _version == AppVersion.Full ? (IDisplayParameter)new ChartParamViewModel<int, int>(recvParam39, dispsetParameter39, i => i, ParameterLogType.Analogue, _parameterLogger, timeCountersGroup.DisplayName) : new AggregateParamViewModel<int>(dispsetParameter39, timeCountersGroup.DisplayName);


			timeCountersGroup.AddParameterOrGroup(setParam39Vm);

			//timeCountersGroup.AddParameterOrGroup(timeCountersGroup);
			return timeCountersGroup;
		}
	}
}