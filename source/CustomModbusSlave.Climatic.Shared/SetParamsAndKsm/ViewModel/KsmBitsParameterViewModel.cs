﻿using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;
using ParamCentric.Modbus.Contracts;
using ParamCentric.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel
{
    public class KsmBitsParameterViewModel : ViewModelBase, IReceivableParameter, IDisplayableParameter<string>
    {
        private readonly int _zbParameterNumber;
        private readonly ITextFormatter<BytesPair?> _currentValueFormatter;
        private string _receivedValueFormatted;

        public KsmBitsParameterViewModel(int zbParameterNumber, string name, ITextFormatter<BytesPair?> currentValueFormatter, IList<IKsmBitParameterViewModel> bits)
        {
            _zbParameterNumber = zbParameterNumber;
            Name = name;
            Bits = bits;
            _currentValueFormatter = currentValueFormatter;
        }

        public string Name { get; }

        public IList<IKsmBitParameterViewModel> Bits { get; }

        public bool ShowBits
        {
            get
            {
                if (Bits == null || Bits.Count == 0) return false;
                return true;
            }
        }

        public BytesPair? ReceivedBytesValue
        {
            set
            {
                if (Bits == null) return;
                foreach (var bit in Bits)
                {
                    bit.ReceivedBytesValue = value;
                }

                ReceivedValueFormatted = _currentValueFormatter.Format(value);
            }
        }

        public string ReceivedValueFormatted
        {
            get => _receivedValueFormatted;
            private set
            {
                if (_receivedValueFormatted != value)
                {
                    _receivedValueFormatted = value;
                    RaisePropertyChanged(() => ReceivedValueFormatted);
                }
            }
        }
    }
}