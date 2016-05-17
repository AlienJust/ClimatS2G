using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter, IAllParametersAccepter {
		private readonly IThreadNotifier _notifier;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<KsmCommonWritableParameterViewModel> _parameterVmList;

		public KsmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<KsmCommonWritableParameterViewModel>();

			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(0, "Датчик 1wire №1 ", new UshortToDoubleConverterNcalc("UshRv * 0.01"), new DoubleTextFormatterSimple("f2", "хз"), false));
			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(1, "Датчик 1wire №2 ", new UshortToDoubleConverterNcalc("UshRv * 0.01"), new DoubleTextFormatterSimple("f2", "хз"), false));

			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(2, "Резерв", new UshortToDoubleConverterNcalc("UshRv * 1.0"), new DoubleTextFormatterSimple("f0", "хз"), false));
			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(3, "Резерв", new UshortToDoubleConverterNcalc("UshRv * 1.0"), new DoubleTextFormatterSimple("f0", "хз"), false));

			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(4, "PIC порт B", new UshortToDoubleConverterNcalc("UshRv * 1.0"), new DoubleTextFormatterBits("хxxxxxxx"), true));

			for (int i = 5; i < 60; ++i) {
				_parameterVmList.Add(new KsmCommonWritableParameterViewModel(i, "Параметр " + (i + 1), new UshortToDoubleConverterNcalc("UshRv * 0.01"), new DoubleTextFormatterSimple("f2", "хз"), false));
			}
		}

		public List<KsmCommonWritableParameterViewModel> ParameterVmList => _parameterVmList;

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			// тут должна быть очередь потокобезопасная
			// нужен поток для обработки ответов - сразу использовать UiNotifier
			_itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
		}

		public void AcceptCommandAllParameters(IList<byte> bytes) {
			// update all parameters
			for (int i = 0; i < 60; ++i) {
				_parameterVmList[i].SetCurrentValue((ushort)(bytes[7 + i*2]*256.0 + bytes[8 + i*2]));
			}
		}
	}
}
