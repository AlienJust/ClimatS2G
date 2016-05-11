using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter {
		private readonly IThreadNotifier _notifier;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<KsmWritableParameterViewModel> _parameterVmList;

		public KsmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<KsmWritableParameterViewModel>();
			for (int i = 0; i < 50; ++i) {
				_parameterVmList.Add(new KsmWritableParameterViewModel(i, "Параметр " + (i + 1), this, new DoubleUshortConverterSimple()));
			}
		}

		public List<KsmWritableParameterViewModel> ParameterVmList => _parameterVmList;

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			// тут должна быть очередь потокобезопасная
			// нужен поток для обработки ответов - сразу использовать UiNotifier
			_itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
		}
	}
}
