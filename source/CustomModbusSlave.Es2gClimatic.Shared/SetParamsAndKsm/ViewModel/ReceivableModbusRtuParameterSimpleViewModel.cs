using AlienJust.Support.Collections;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel {
	public class ReceivableModbusRtuParameterSimpleViewModel : ViewModelBase, IReceivableModbusRtuParameter, IDisplayableParameter<string> {
		private readonly IBytesPairNullableSomethingConverter<string> _toDisplayConverter;
		private BytesPair? _receivedBytesValue;

		public ReceivableModbusRtuParameterSimpleViewModel(string name, byte address, byte commandCode, int zeroBasedParameterNumber, IBytesPairNullableSomethingConverter<string> toDisplayConverter) {
			Name = name;
			Address = address;
			CommandCode = commandCode;
			ZeroBasedParameterNumber = zeroBasedParameterNumber;

			_toDisplayConverter = toDisplayConverter;
		}

		public string Name { get; }

		public BytesPair? ReceivedBytesValue {
			set
			{
				if (_receivedBytesValue != value) {
					_receivedBytesValue = value;
					RaisePropertyChanged(()=> ReceivedValueFormatted);
				}
			}
		}

		public byte CommandCode { get; }

		public byte Address { get; }

		public int ZeroBasedParameterNumber { get; }

		public string ReceivedValueFormatted => _toDisplayConverter.ConvertToSomething(_receivedBytesValue); // TODO: fix nulable case
	}
}