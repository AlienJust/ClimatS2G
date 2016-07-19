using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.TextFormatters;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.ViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter, IAllParametersAccepter {
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly List<IReceivableParameter> _parameterVmList;
		//private readonly List<SettableParameterViewModel> _settableParameterVmList;
		private const string UnknownBits = "xxxx xxxx xxxx xxxx";

		public KsmDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>();
			_parameterVmList = new List<IReceivableParameter> {
				new KsmReadonlyParamViewModel(0, "Датчик 1wire №1", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85,0x00), "f2", "хз", "обрыв"))
				, new KsmReadonlyParamViewModel(1, "Датчик 1wire №2 ", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85,0x00), "f2", "хз", "обрыв"))
				, new KsmReadonlyParamViewModel(2, "Датчик 1wire №3", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85,0x00), "f2", "хз", "обрыв"))
				, new KsmReadonlyParamViewModel(3, "Датчик 1wire №4", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85,0x00), "f2", "хз", "обрыв"))
				, new KsmReadonlyParamViewModel(4, "Датчик 1wire №5", new TextFormatterSensor(0.01, 0.0, new BytesPair(0x85,0x00), "f2", "хз", "обрыв"))
				, new KsmBitsParameterViewModel(5, "PIC порт A", new TextFormatterBits(UnknownBits)
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "PA.0"),
						new KsmBitParameterViewModel(1, "PA.1"),
						new KsmBitParameterViewModel(2, "PA.2"),
						new KsmBitParameterViewModel(3, "PA.3"),
						new KsmBitParameterViewModel(4, "PA.4"),
						new KsmBitParameterViewModel(5, "PA.5"),
						new KsmBitParameterViewModel(6, "PA.6"),
						new KsmBitParameterViewModel(7, "PA.7")
					})
				, new KsmBitsParameterViewModel(6, "PIC порт C", new TextFormatterBits(UnknownBits)
					, new List<IKsmBitParameterViewModel> {
						new KsmBitParameterViewModel(0, "PC.0"),
						new KsmBitParameterViewModel(1, "PC.1"),
						new KsmBitParameterViewModel(2, "PC.2"),
						new KsmBitParameterViewModel(3, "PC.3"),
						new KsmBitParameterViewModel(4, "PC.4"),
						new KsmBitParameterViewModel(5, "PC.5"),
						new KsmBitParameterViewModel(6, "PC.6"),
						new KsmBitParameterViewModel(7, "PC.7")
					})
				, new KsmBitsParameterViewModel(7, "Диагностика контроллера (ErrorsFlags)", new TextFormatterBits(UnknownBits), null)
				, new KsmReadonlyParamViewModel(8, "Этап работы", new TextFormatterSimple("f2", "хз"))

				, new KsmBitsParameterViewModel(9, "Регистр аварий в режиме включено", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(10, "Регистр аварий в режиме резерв", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(11, "Регистр аварий в режиме отстой", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(12, "Регистр аварий в режиме обслуживание", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(13, "Регистр аварий в режиме мойка", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(14, "Регистр аварий в режиме ручной", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(15, "Регистр аварий в режиме выключено", new TextFormatterBits(UnknownBits), null)
				
				, new KsmBitsParameterViewModel(16, "Состояние обмена 1 (ErrorsResponse)", new TextFormatterBits(UnknownBits), null)
				, new KsmBitsParameterViewModel(17, "Состояние обмена 2 (ErrorsResponse)", new TextFormatterBits(UnknownBits), null)
				, new KsmReadonlyParamViewModel(18, "Уставка ШИМ на клапан", new TextFormatterSimple("f2", "хз"))
			};

			for (int i = 19; i < 28; ++i) {
				_parameterVmList.Add(new KsmReadonlyParamViewModel(i, "Параметр " + (i + 1), new TextFormatterSimple("f2", "хз")));
			}
			_parameterVmList.Add(new KsmReadonlyParamViewModel(34, "Версия ПО", new TextFormatterSimple("f0", "хз")));

			
			//_settableParameterVmList = new List<SettableParameterViewModel>();

			_parameterVmList.Add(new SettableParameterViewModel(28, "Максимальный ШИМ (PWMmax_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier));
			_parameterVmList.Add(new SettableParameterViewModel(29, "Минимальный ШИМ (PWMmin_cool)", 255, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier));

			_parameterVmList.Add(new SettableParameterViewModel(30, "Дельта + (plus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier));
			_parameterVmList.Add(new SettableParameterViewModel(31, "Дельта - (minus_cool)", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier));

			for (int i = 32; i < 50; ++i) {
				_parameterVmList.Add(new SettableParameterViewModel(i, "Параметр", 65535, 0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, notifier));
			}
		}

		public List<IReceivableParameter> ParameterVmList => _parameterVmList;

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			_itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
		}

		public void AcceptCommandAllParameters(IList<byte> bytes) {
			// update all parameters
			for (int i = 0; i < _parameterVmList.Count; ++i) {
				_parameterVmList[i].ReceivedBytesValue = new BytesPair(bytes[7 + i * 2], bytes[8 + i * 2]);//(ushort)(bytes[7 + i * 2] * 256.0 + bytes[8 + i * 2]));
			}
		}
	}
}
