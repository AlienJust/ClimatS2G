using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Ksm.WamOrCoolForcedModes;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<IReceivableParameter> _parameterVmList;

		//private readonly List<SettableParameterViewModel> _settableParameterVmList;
		private const string UnknownBits = "xxxx xxxx xxxx xxxx";
		public AnyCommandPartViewModel DataAsText { get; }

		public KsmDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IList<BytesPair>> cmdListenerKsmParams) {
			_notifier = notifier;
			_cmdListenerKsmParams = cmdListenerKsmParams;

			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<IReceivableParameter> {
				new KsmReadonlyParamViewModel(0, "Датчик 1wire №1", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
				new KsmReadonlyParamViewModel(1, "Датчик 1wire №2 ", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
				new KsmReadonlyParamViewModel(2, "Резерв", new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")),
				new KsmReadonlyParamViewModel(3, "Резерв", new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")), new KsmBitsParameterViewModel(4, "PIC порт B", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "PB.0 = 0: сегмент — мастер, иначе слейв"),
					new KsmBitParameterViewModel(1, "PB.1 = 1: пожар"),
					new KsmBitParameterViewModel(2, "PB.2 = 0: 100% нагрев калорифера 380 в и калорифера 3кВ с включением вентилятора испарителя"),
					new KsmBitParameterViewModel(3, "PB.3 = 0: 50 % нагрев 3 кВ калорифера"),
					new KsmBitParameterViewModel(4, "PB.4 = 0: 100% расход вентилятора испарителя"),
					new KsmBitParameterViewModel(5, "PB.5 = 0: 50 % охлаждение (30 кВт компрессора) с включением вентиляторов испарителя и конденсатора"),
					new KsmBitParameterViewModel(6, "PB.6 = 0: 100 % охлаждение (20 кВт и 30 кВт компрессоров) с включением вентиляторов испарителя и конденсатора"),
				}),
				new KsmBitsParameterViewModel(5, "PIC порт A", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(4, "PA.4 = 0: Подогрев картера, включение"),
				}),
				new KsmBitsParameterViewModel(6, "PIC порт C", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(2, "PC.2 = 0: Клапан разгрузки кондиционера, включение"),
					new KsmBitParameterViewModel(6, "PC.6 = 0: Включение реле разрешения подачи питания на компрессор 1 (20 кВ)"),
					new KsmBitParameterViewModel(7, "PC.7 = 0: Включение обеззараживателя"),
				}),
				new KsmBitsParameterViewModel(7, "Диагностика контроллера (ErrorsFlags)", new TextFormatterBits(UnknownBits), null),
				new KsmReadonlyParamViewModel(8, "Этап работы", new TextFormatterWorkStage()), new KsmBitsParameterViewModel(9, "Регистр аварий 1 в режиме включено", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - Резерв"),
					new KsmBitParameterViewModel(1, "b.1 - Не пройден тест заслонки зима лето"),
					new KsmBitParameterViewModel(2, "b.2 - Не пройден тест заслонки рециркуляционного воздуха"),
					new KsmBitParameterViewModel(3, "b.3 - Не пройден тест вытяжного вентилятора"),
					new KsmBitParameterViewModel(4, "b.4 - Не пройден тест вентилятора конденсатора"),
					new KsmBitParameterViewModel(5, "b.5 - Не пройден тест вентилятора испарителя"),
					new KsmBitParameterViewModel(6, "b.6 - Не пройден тест заслонки наружного воздуха"),
					new KsmBitParameterViewModel(7, "b.7 - Нет 380 вольт"),
				}),
				new KsmBitsParameterViewModel(10, "Регистр аварий 2 в режиме включено", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - Резерв"),
					new KsmBitParameterViewModel(1, "b.1 - Резерв"),
					new KsmBitParameterViewModel(2, "b.2 - Не пройден контроль контактора компрессора 2"),
					new KsmBitParameterViewModel(3, "b.3 - Не пройден контроль контактора компрессора 1"),
					new KsmBitParameterViewModel(4, "b.4 - Автомат 380 В выключен компрессора 2"),
					new KsmBitParameterViewModel(5, "b.5 - Автомат 380 В выключен компрессора 1"),
					new KsmBitParameterViewModel(6, "b.6 - Датчик высокого давления МУКа конденсатора не в диапазоне"),
					new KsmBitParameterViewModel(7, "b.7 - Датчик низкого давления и температуры от Emerson не в диапазоне")
				}),
				new KsmBitsParameterViewModel(11, "Регистр аварий 3 в режиме включено", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - Срабатывание аппаратного отключение компрессора 2 по Pmax"),
					new KsmBitParameterViewModel(1, "b.1 - Срабатывание аппаратного отключение компрессора 2 по Pmin"),
					new KsmBitParameterViewModel(2, "b.2 - Срабатывание термостата компрессора 2"),
					new KsmBitParameterViewModel(3, "b.3 - Не пройден контроль разрешение работы компрессора 2"),
					new KsmBitParameterViewModel(4, "b.4 - Срабатывание аппаратного отключение компрессора 1 по Pmax"),
					new KsmBitParameterViewModel(5, "b.5 - Срабатывание аппаратного отключение компрессора 1 по Pmin"),
					new KsmBitParameterViewModel(6, "b.6 - Срабатывание термостата компрессора 1"),
					new KsmBitParameterViewModel(7, "b.7 - Не пройден контроль разрешение работы компрессора 1")
				}),
				new KsmBitsParameterViewModel(12, "Регистр аварий в режиме резерв", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(13, "Регистр аварий в режиме отстой", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(14, "Регистр аварий 1 в режиме обслуживание", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(15, "Регистр аварий 2 в режиме обслуживание", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(16, "Регистр аварий 3 в режиме обслуживание", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(17, "Регистр аварий в режиме мойка", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(18, "Регистр аварий 1 в режиме ручной", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(19, "Регистр аварий 2 в режиме ручной", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(20, "Регистр аварий 3 в режиме ручной", new TextFormatterBits(UnknownBits), null),
				new KsmBitsParameterViewModel(21, "Регистр аварий в режиме выключено", new TextFormatterBits(UnknownBits), null), new KsmBitsParameterViewModel(22, "Состояние обмена 1 (ErrorsResponse), 1 = ошибка обмена", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - вычитка текущих данных из МУК заслонки наружного воздуха"),
					new KsmBitParameterViewModel(1, "b.1 - запись команд в МУК заслонки наружного воздуха"),
					new KsmBitParameterViewModel(2, "b.2 - вычитка текущих данных из МУК испарителя"),
					new KsmBitParameterViewModel(3, "b.3 - запись команд в МУК испарителя"),
					new KsmBitParameterViewModel(4, "b.4 - вычитка текущих данных из МУК конденсатора"),
					new KsmBitParameterViewModel(5, "b.5 - запись команд в МУК конденсатора"),
					new KsmBitParameterViewModel(6, "b.6 - вычитка текущих данных из МУК вытяжного вентилятора"),
					new KsmBitParameterViewModel(7, "b.7 - запись команд в МУК вытяжного вентилятора")
				}),
				new KsmBitsParameterViewModel(23, "Состояние обмена 2 (ErrorsResponse), 1 = ошибка обмена", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - вычитка текущих данных из МУК рециркуляционной заслонки"),
					new KsmBitParameterViewModel(1, "b.1 - запись команд в МУК рециркуляционной заслонки"),
					new KsmBitParameterViewModel(2, "b.2 - вычитка текущих данных из МУК заслонки зима-лето"),
					new KsmBitParameterViewModel(3, "b.3 - запись команд в МУК заслонки зима-лето"),
					new KsmBitParameterViewModel(4, "b.4 - обмен с БС-СМ"),
					new KsmBitParameterViewModel(5, "b.5 - обмен с БВС"),
					new KsmBitParameterViewModel(6, "b.6 - обмен с БВС2"),
					new KsmBitParameterViewModel(7, "b.7 - запись данных в РПД")
				}),
				new KsmBitsParameterViewModel(24, "Состояние обмена 3 (ErrorsResponse), 1 = ошибка обмена", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel> {
					new KsmBitParameterViewModel(0, "b.0 - вычитка корректируемого параметра из программы технического абонента"),
					new KsmBitParameterViewModel(1, "b.1 - отсылка текущих данных техническому абоненту")
				}),
				new KsmReadonlyParamViewModel(25, "Уставка ШИМ на клапан разгрузки", new TextFormatterNcalcDouble("UshRv * 1.0", "f0", "хз")),
				new KsmReadonlyParamViewModel(26, "Ручной принудительный режим", new TextFormatterForcedManualMode())
			};

			for (int i = 27; i < 34; ++i) {
				_parameterVmList.Add(new KsmReadonlyParamViewModel(i, "Параметр " + (i + 1), new TextFormatterSimple("f2", "хз")));
			}

			_parameterVmList.Add(new KsmReadonlyParamViewModel(34, "Версия ПО", new TextFormatterSimple("f0", "хз")));


			//_settableParameterVmList = new List<SettableParameterViewModel>();

			_parameterVmList.Add(new SettableParameterViewModel(35, "Максимальный ШИМ (PWMmax_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(36, "Минимальный ШИМ (PWMmin_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

			_parameterVmList.Add(new SettableParameterViewModel(37, "Дельта + (plus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(38, "Дельта - (minus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

			_parameterVmList.Add(new SettableParameterViewModel(39, "Обеззараживатель, почасовой счетчик работы (CounterCleaner)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(40, "Компрессор этого сегмента, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(41, "Компрессор, счетчик перевключений", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(42, "Вентилятор конденсатора 1, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(43, "Вентилятор конденсатора 2, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(44, "Отопитель 380 в., почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(45, "Отопитель 3000 в., почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(46, "Вентилятор приточного воздуха, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			_parameterVmList.Add(new SettableParameterViewModel(47, "Вентилятор отработанного воздуха, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

			var converter = new WarmOrCoolForcedModeToStringConverter();
			var bpConverter = new BuilderOneToOneFunc<BytesPair, string>(bp => {
				try {
					Console.WriteLine("Need to convert " + bp.HighFirstSignedValue + " to WarmOrCoolForcedMode");
					return converter.Build((WarmOrCoolForcedMode)bp.HighFirstSignedValue);
				}
				catch {
					Console.WriteLine("Cannot convert value " + bp + " to WarmOrCoolForcedMode");
					return converter.Build(WarmOrCoolForcedMode.Unknown);
				}
			});

			_parameterVmList.Add(new WarmOrCoolForcedModeViewModel(48, "Принудительный режим обогрев/охлаждение", null, bpConverter, new List<IRawAndConvertedValues<BytesPair, string>> {
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 0), converter.Build(WarmOrCoolForcedMode.AutomaticMode)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 1), converter.Build(WarmOrCoolForcedMode.Cool100Percent)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 2), converter.Build(WarmOrCoolForcedMode.Cool50Percent)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 3), converter.Build(WarmOrCoolForcedMode.Ventilation)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 4), converter.Build(WarmOrCoolForcedMode.Warm100Percent)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 5), converter.Build(WarmOrCoolForcedMode.Warm50Percent)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 6), converter.Build(WarmOrCoolForcedMode.TurnOnUov)),
				new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 7), converter.Build(WarmOrCoolForcedMode.Test3000VoltageHeating))
			}, "Режим работы", parameterSetter, _notifier, new BuilderOneToOneFunc<BytesPair, BytesPair>(bp => bp)));

			_parameterVmList.Add(new SettableParameterViewModel(49, "Дельта уставки температуры, отладка", 32767 * 0.1 - 2.5, -32767 * 0.1 - 2.5, null, "f1", new DoubleBytesPairConverterModifyAndOffsetUshort(0.1, -2.5), parameterSetter, notifier, null));

			for (int i = 50; i < 60; ++i) {
				_parameterVmList.Add(new SettableParameterViewModel(i, "Параметр", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
			}

			DataAsText = new AnyCommandPartViewModel();
			_cmdListenerKsmParams.DataReceived += CmdListenerKsmParamsOnDataReceived;
		}

		private void CmdListenerKsmParamsOnDataReceived(IList<byte> bytes, IList<BytesPair> data) {
			_notifier.Notify(() => {
				for (int i = 0; i < 60; ++i) {
					_parameterVmList[i].ReceivedBytesValue = data[i];
				}

				DataAsText.Update(bytes);
			});
		}

		public List<IReceivableParameter> ParameterVmList => _parameterVmList;

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			// тут должна быть очередь потокобезопасная
			// нужен поток для обработки ответов - сразу использовать UiNotifier
			_itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
		}
	}
}


