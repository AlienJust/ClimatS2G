using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter, IAllParametersAccepter {
		private readonly IThreadNotifier _notifier;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<IKsmParameterViewModel> _parameterVmList;

		public KsmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<IKsmParameterViewModel> {
				new KsmCommonWritableParameterViewModel(0, "Датчик 1wire №1", new TextFormatterNcalcDouble("UshRv * 0.01", "f2", "хз"))
				, new KsmCommonWritableParameterViewModel(1, "Датчик 1wire №2 ", new TextFormatterNcalcDouble("UshRv * 0.01", "f2", "хз"))
				, new KsmCommonWritableParameterViewModel(2, "Резерв" /*, new UshortToDoubleConverterNcalc("UshRv * 1.0")*/, new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")), new KsmCommonWritableParameterViewModel(3, "Резерв" /*, new UshortToDoubleConverterNcalc("UshRv * 1.0")*/, new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз"))
				, new KsmBitsParameterViewModel(4, "PIC порт B", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "PB.0 = 0: сегмент — мастер, иначе слейв"),
						new KsmBitParameterViewModel(1, "PB.1 = 0: пожар"),
						new KsmBitParameterViewModel(2, "PB.2 = 0: 100% нагрев калорифера 380 в и калорифера 3кВ с включением вентилятора испарителя"),
						new KsmBitParameterViewModel(3, "PB.3 = 0: 50 % нагрев 3 кВ калорифера"),
						new KsmBitParameterViewModel(4, "PB.4 = 0: 100% расход вентилятора испарителя"),
						new KsmBitParameterViewModel(5, "PB.5 = 0: 50 % охлаждение (30 кВт компрессора) с включением вентиляторов испарителя и конденсатора"),
						new KsmBitParameterViewModel(6, "PB.6 = 0: 100 % охлаждение (20 кВт и 30 кВт компрессоров) с включением вентиляторов испарителя и конденсатора"),
					})
				, new KsmBitsParameterViewModel(5, "PIC порт A", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(4, "PA.4 = 0: Подогрев картера, включение"),
					})
				, new KsmBitsParameterViewModel(6, "PIC порт C", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(2, "PC.2 = 0: Клапан разгрузки кондиционера, включение"),
						new KsmBitParameterViewModel(6, "PC.6 = 0: Включение реле разрешения подачи питания на компрессор 1 (20 кВ)"),
						new KsmBitParameterViewModel(7, "PC.7 = 0: Включение обеззараживателя"),
					})
				, new KsmBitsParameterViewModel(7, "Диагностика контроллера (ErrorsFlags)", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
				, new KsmCommonWritableParameterViewModel(8, "Этап работы", new TextFormatterWorkStage())

				, new KsmBitsParameterViewModel(9, "Регистр аварий 1 в режиме включено", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "b.0 - Резерв"),
						new KsmBitParameterViewModel(1, "b.1 - Не пройден тест заслонки зима лето"),
						new KsmBitParameterViewModel(2, "b.2 - Не пройден тест заслонки рециркуляционного воздуха"),
						new KsmBitParameterViewModel(3, "b.3 - Не пройден тест вытяжного вентилятора"),
						new KsmBitParameterViewModel(4, "b.4 - Не пройден тест вентилятора конденсатора"),
						new KsmBitParameterViewModel(5, "b.5 - Не пройден тест вентилятора испарителя"),
						new KsmBitParameterViewModel(6, "b.6 - Не пройден тест заслонки наружного воздуха"),
						new KsmBitParameterViewModel(7, "b.7 - Нет 380 вольт"),
					})
					, new KsmBitsParameterViewModel(10, "Регистр аварий 2 в режиме включено", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "b.0 - Резерв"),
						new KsmBitParameterViewModel(1, "b.1 - Резерв"),
						new KsmBitParameterViewModel(2, "b.2 - Не пройден контроль контактора компрессора 2"),
						new KsmBitParameterViewModel(3, "b.3 - Не пройден контроль контактора компрессора 1"),
						new KsmBitParameterViewModel(4, "b.4 - Автомат 380 В выключен компрессора 2"),
						new KsmBitParameterViewModel(5, "b.5 - Автомат 380 В выключен компрессора 1"),
						new KsmBitParameterViewModel(6, "b.6 - Датчик высокого давления МУКа конденсатора не в диапазоне"),
						new KsmBitParameterViewModel(7, "b.7 - Датчик низкого давления и температуры от Emerson не в диапазоне"),
					})
					, new KsmBitsParameterViewModel(11, "Регистр аварий 3 в режиме включено", new TextFormatterBits("хxxxxxxx хxxxxxxx")
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "b.0 - Срабатывание аппаратного отключение компрессора 2 по Pmax"),
						new KsmBitParameterViewModel(1, "b.1 - Срабатывание аппаратного отключение компрессора 2 по Pmin"),
						new KsmBitParameterViewModel(2, "b.2 - Срабатывание термостата компрессора 2"),
						new KsmBitParameterViewModel(3, "b.3 - Не пройден контроль разрешение работы компрессора 2"),
						new KsmBitParameterViewModel(4, "b.4 - Срабатывание аппаратного отключение компрессора 1 по Pmax"),
						new KsmBitParameterViewModel(5, "b.5 - Срабатывание аппаратного отключение компрессора 1 по Pmin"),
						new KsmBitParameterViewModel(6, "b.6 - Срабатывание термостата компрессора 1"),
						new KsmBitParameterViewModel(7, "b.7 - Не пройден контроль разрешение работы компрессора 1"),
					})
					, new KsmBitsParameterViewModel(12, "Регистр аварий в режиме резерв", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(13, "Регистр аварий в режиме отстой", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(14, "Регистр аварий 1 в режиме обслуживание", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(15, "Регистр аварий 2 в режиме обслуживание", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(16, "Регистр аварий 3 в режиме обслуживание", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(17, "Регистр аварий в режиме мойка", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(18, "Регистр аварий 1 в режиме ручной", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(19, "Регистр аварий 2 в режиме ручной", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(20, "Регистр аварий 3 в режиме ручной", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)
					, new KsmBitsParameterViewModel(21, "Регистр аварий в режиме выключено", new TextFormatterBits("хxxxxxxx хxxxxxxx"), null)

			};

			for (int i = 22; i < 60; ++i) {
				_parameterVmList.Add(new KsmCommonWritableParameterViewModel(i, "Параметр " + (i + 1), new TextFormatterSimple("f2", "хз")));
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
				_parameterVmList[i].SetCurrentValue((ushort)(bytes[7 + i * 2] * 256.0 + bytes[8 + i * 2]));
			}
		}
	}
}
