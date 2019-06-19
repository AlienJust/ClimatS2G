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

namespace CustomModbusSlave.Es2gClimatic.CabinApp.Ksm
{
    class KsmDataViewModel : ViewModelBase, IParameterSetter
    {
        private readonly IThreadNotifier _notifier;
        private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;
        private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;

        private readonly List<IReceivableParameter> _parameterVmList;

        //private readonly List<SettableParameterViewModel> _settableParameterVmList;
        private const string UnknownBits = "xxxx xxxx xxxx xxxx";
        public AnyCommandPartViewModel DataAsText { get; }

        public KsmDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IList<BytesPair>> cmdListenerKsmParams)
        {
            _notifier = notifier;
            _cmdListenerKsmParams = cmdListenerKsmParams;
            _itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
            _parameterVmList = new List<IReceivableParameter>
            {
                new KsmReadonlyParamViewModel(0, "Датчик 1wire №1",
                    new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
                new KsmReadonlyParamViewModel(1, "Датчик 1wire №2 ",
                    new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
                new KsmReadonlyParamViewModel(2, "Датчик 1wire №3",
                    new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
                new KsmReadonlyParamViewModel(3, "Датчик 1wire №4",
                    new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),
                new KsmReadonlyParamViewModel(4, "Датчик 1wire №5",
                    new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85, 0x00), "f2", "хз", "обрыв")),

                new KsmBitsParameterViewModel(5, "PIC порт A", new TextFormatterBits(UnknownBits),
                    new List<IKsmBitParameterViewModel>
                {
                    new KsmBitParameterViewModel(4, "PA.4 = 0. Подогрев картера, включение"),
                }),

                new KsmBitsParameterViewModel(6, "PIC порт C", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel>
                {
                    new KsmBitParameterViewModel(2, "PC.2 = 0: Клапан разгрузки кондиционера, включение"),
                    new KsmBitParameterViewModel(6, "PC.6 = 0: Включение реле разрешения подачи питания на компрессор"),
                }),
                new KsmBitsParameterViewModel(7, "Диагностика контроллера (ErrorsFlags)", new TextFormatterBits(UnknownBits), null),
                new KsmReadonlyParamViewModel(8, "Этап работы", new TextFormatterSimple("f2", "хз")),
                new KsmBitsParameterViewModel(9, "Регистр аварий в режиме включено", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(10, "Регистр аварий в режиме резерв", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(11, "Регистр аварий в режиме отстой", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(12, "Регистр аварий в режиме обслуживание", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(13, "Регистр аварий в режиме мойка", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(14, "Регистр аварий в режиме ручной", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(15, "Регистр аварий в режиме выключено", new TextFormatterBits(UnknownBits), null),
                new KsmBitsParameterViewModel(16, "Состояние обмена 1 (ErrorsResponse), 1 = ошибка обмена", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel>
                {
                    new KsmBitParameterViewModel(0, "b.0 - вычитка текущих данных из МУК заслонки наружного воздуха"),
                    new KsmBitParameterViewModel(1, "b.1 - запись команд в МУК заслонки наружного воздуха"),
                    new KsmBitParameterViewModel(2, "b.2 - вычитка текущих данных из МУК испарителя"),
                    new KsmBitParameterViewModel(3, "b.3 - запись команд в МУК испарителя"),
                    new KsmBitParameterViewModel(4, "b.4 - вычитка текущих данных из МУК конденсатора"),
                    new KsmBitParameterViewModel(5, "b.5 - запись команд в МУК конденсатора"),
                    new KsmBitParameterViewModel(6, "b.6 - вычитка текущих данных из МУК теплого пола"),
                    new KsmBitParameterViewModel(7, "b.7 - запись команд в МУК теплого пола")
                }),
                new KsmBitsParameterViewModel(17, "Состояние обмена 2 (ErrorsResponse), 1 = ошибка обмена", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel>
                {
                    new KsmBitParameterViewModel(0, "b.0 - обмен с БС-СМ"),
                    new KsmBitParameterViewModel(1, "b.1 - обмен с БВС"),
                    new KsmBitParameterViewModel(2, "b.2 - запись данных в РПД"),
                    new KsmBitParameterViewModel(3, "b.3 - вычитка корректируемого параметра из программы технического абонента"),
                    new KsmBitParameterViewModel(4, "b.4 - отсылка текущих данных техническому абоненту")
                }),
                new KsmReadonlyParamViewModel(18, "Уставка ШИМ на клапан", new TextFormatterSimple("f2", "хз")),
                new KsmReadonlyParamViewModel(19, "Ручной принудительный режим", new TextFormatterSimple("f2", "хз")),
                new KsmBitsParameterViewModel(20, "PIC порт B", new TextFormatterBits(UnknownBits), new List<IKsmBitParameterViewModel>
                {
                    new KsmBitParameterViewModel(1, "PB.1 = 1: аварийное отключение"),
                })
            };

            for (int i = 21; i < 27; ++i)
            {
                _parameterVmList.Add(new KsmReadonlyParamViewModel(i, "Параметр " + i, new TextFormatterSimple("f2", "хз")));
            }

            _parameterVmList.Add(new KsmReadonlyParamViewModel(27, "Версия ПО", new TextFormatterDotted("хз")));

            _parameterVmList.Add(new SettableParameterViewModel(28, "Максимальный ШИМ (PWMmax_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(29, "Минимальный ШИМ (PWMmin_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

            _parameterVmList.Add(new SettableParameterViewModel(30, "Дельта + (plus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(31, "Дельта - (minus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

            _parameterVmList.Add(new SettableParameterViewModel(32, "Компрессор,  почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(33, "Вентилятор конденсатора, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(34, "Отопитель 380 в., почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(35, "Вентилятор приточного воздуха, почасовой счетчик работы", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));
            _parameterVmList.Add(new SettableParameterViewModel(36, "Дельта уставки температуры, отладка, ЦМР 0.1 0 = -2.5, 50 = +2.5", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier, null));

            var converter = new WarmOrCoolForcedModeToStringConverter();
            var bpConverter = new BuilderOneToOneFunc<BytesPair, string>(bp =>
            {
                try
                {
                    //Console.WriteLine("Need to convert " + bp.HighFirstSignedValue + " to WarmOrCoolForcedMode");
                    return converter.Build((WarmOrCoolForcedMode)bp.HighFirstSignedValue);
                }
                catch
                {
                    //Console.WriteLine("Cannot convert value " + bp + " to WarmOrCoolForcedMode");
                    return converter.Build(WarmOrCoolForcedMode.Unknown);
                }
            });

            _parameterVmList.Add(new WarmOrCoolForcedModeViewModel(37, "Принудительный режим обогрев/охлаждение", null, bpConverter,
                new List<IRawAndConvertedValues<BytesPair, string>>
                {
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 0), converter.Build(WarmOrCoolForcedMode.AutomaticMode)),
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 1), converter.Build(WarmOrCoolForcedMode.Cool100Percent)),
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 2), converter.Build(WarmOrCoolForcedMode.Cool50Percent)),
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 3), converter.Build(WarmOrCoolForcedMode.Ventilation)),
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 4), converter.Build(WarmOrCoolForcedMode.Warm100Percent)),
                    new RawAndConvertedValuesSimple<BytesPair, string>(new BytesPair(0, 5), converter.Build(WarmOrCoolForcedMode.Warm50Percent))
                },
                "Режим работы", parameterSetter, _notifier, new BuilderOneToOneFunc<BytesPair, BytesPair>(bp => bp)));

            for (int i = 38; i < 50; ++i)
            {
                _parameterVmList.Add(
                    new SettableParameterViewModel(i, "Параметр", 65535, 0, null, "f0",
                                                   new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier,
                                                   null));
            }

            DataAsText = new AnyCommandPartViewModel();
            _cmdListenerKsmParams.DataReceived += CmdListenerKsmParamsOnDataReceived;
        }

        private void CmdListenerKsmParamsOnDataReceived(IList<byte> bytes, IList<BytesPair> data)
        {
            _notifier.Notify(() =>
            {
                for (int i = 0; i < _parameterVmList.Count; ++i)
                {
                    _parameterVmList[i].ReceivedBytesValue = data[i];
                }

                DataAsText.Update(bytes);
            });
        }

        public List<IReceivableParameter> ParameterVmList => _parameterVmList;

        public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback)
        {
            _itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
        }
    }
}