﻿using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm.Fault;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.SystemDiagnostic
{
    class BsSmFaultViewModel : ViewModelBase
    {
        private int _code;

        public int Code
        {
            get => _code;
            set
            {
                if (_code != value)
                {
                    _code = value;
                    RaisePropertyChanged(() => Code);
                    RaisePropertyChanged(() => Description);
                }
            }
        }

        public string Description => IntegerFaultCodeToTextConverter.Convert(Code);
    }
}