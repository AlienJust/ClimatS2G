using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter, IAllParametersAccepter {
		private readonly IThreadNotifier _notifier;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<IKsmParameterViewModel> _parameterVmList;

		public KsmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<IKsmParameterViewModel>();

			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(0, "Датчик 1wire №1 "/*, new UshortToDoubleConverterNcalc("UshRv * 0.01")*/, new TextFormatterNcalcDouble("UshRv * 0.01", "f2", "хз")));
			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(1, "Датчик 1wire №2 "/*, new UshortToDoubleConverterNcalc("UshRv * 0.01")*/, new TextFormatterNcalcDouble("UshRv * 0.01", "f2", "хз")));

			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(2, "Резерв"/*, new UshortToDoubleConverterNcalc("UshRv * 1.0")*/, new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")));
			_parameterVmList.Add(new KsmCommonWritableParameterViewModel(3, "Резерв"/*, new UshortToDoubleConverterNcalc("UshRv * 1.0")*/, new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")));

			_parameterVmList.Add(new KsmBitsParameterViewModel(4, "PIC порт B"/*, new UshortToDoubleConverterNcalc("UshRv * 1.0")*/, new TextFormatterBits("хxxxxxxx")
				, new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "PB.0 = 0: сегмент — мастер, иначе слейв"),
					new KsmBitParameterViewModel(1, "PB.1 = 0: пожар"),
					new KsmBitParameterViewModel(2, "PB.2 = 0: 100% нагрев калорифера 380 в и калорифера 3кВ с включением вентилятора испарителя"),
					new KsmBitParameterViewModel(3, "PB.3 = 0: 50 % нагрев 3 кВ калорифера"),
					new KsmBitParameterViewModel(4, "PB.4 = 0: 100% расход вентилятора испарителя"),
					new KsmBitParameterViewModel(5, "PB.5 = 0: 50 % охлаждение (30 кВт компрессора) с включением вентиляторов испарителя и конденсатора"),
					new KsmBitParameterViewModel(6, "PB.6 = 0: 100 % охлаждение (20 кВт и 30 кВт компрессоров) с включением вентиляторов испарителя и конденсатора"),
				}));

			_parameterVmList.Add(new KsmBitsParameterViewModel(5, "PIC порт A", new TextFormatterBits("хxxxxxxx хxxxxxxx")
				, new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(4, "PA.4 = 0: Подогрев картера, включение"),
				}));

			_parameterVmList.Add(new KsmBitsParameterViewModel(6, "PIC порт C", new TextFormatterBits("хxxxxxxx хxxxxxxx")
				, new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(2, "PC.2 = 0: Клапан разгрузки кондиционера, включение"),
					new KsmBitParameterViewModel(6, "PC.6 = 0:  Включение реле разрешения подачи питания на компрессор 1 (20 кВ)"),
					new KsmBitParameterViewModel(7, "PC.7 = 0: Включение обеззараживателя"),
				}));

			_parameterVmList.Add(new KsmBitsParameterViewModel(7, "Диагностика контроллера (ErrorsFlags)", new TextFormatterBits("хxxxxxxx хxxxxxxx")
				, null));

			for (int i = 8; i < 60; ++i) {
				_parameterVmList.Add(new KsmCommonWritableParameterViewModel(i, "Параметр " + (i + 1)/*, new UshortToDoubleConverterNcalc("UshRv * 0.01")*/, new TextFormatterSimple("f2", "хз")));
			}
		}

		public List<IKsmParameterViewModel> ParameterVmList => _parameterVmList;

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
