using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Wpf;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.Ksm.WamOrCoolForcedModes {
	public class WarmOrCoolForcedModeViewModel : SettableDropdownParameterViewModel<BytesPair>, IReceivableParameter, ISettableBytesPairParameter {
		public WarmOrCoolForcedModeViewModel(int paramIndex, string name, BytesPair? defaultValue, IBuilderOneToOne<BytesPair, string> stringBuilder,
			List<IRawAndConvertedValues<BytesPair, string>> dropItems, string toolTipText, IParameterSetter parameterSetter, IThreadNotifier uiNotifier,
			IBuilderOneToOne<BytesPair, BytesPair> sendConverter)
			: base(paramIndex, name, defaultValue, stringBuilder, dropItems, toolTipText, parameterSetter, uiNotifier, sendConverter) { }

		public BytesPair? ReceivedBytesValue {
			get => ReceivedValueFormatted.RawValue;
			set => SetReceivedValueRaw(value);
		}
		public BytesPair? BytesValue => FormattedValue != null ? new BytesPair?(FormattedValue.RawValue) : null;
	}
}
