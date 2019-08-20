using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;


namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor
{
    internal class MukWarmFloorDataViewModel : ViewModelBase
    {
        private readonly IThreadNotifier _notifier;
        private readonly ICmdListener<IMukWarmFloorReply03Data> _reply03Listener;
        private readonly ICmdListener<IMukWarmFloorRequest16Data> _request16Listener;

        private IMukWarmFloorRequest16Data _request16;
        private IMukWarmFloorReply03Data _reply03;

        public AnyCommandPartViewModel Reply03BytesTextVm { get; set; }
        public AnyCommandPartViewModel Request16BytesTextVm { get; set; }

        public MukWarmFloorSetParamsViewModel MukWarmFloorSetParamsVm { get; }

        public MukWarmFloorDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukWarmFloorReply03Data> reply03Listener, ICmdListener<IMukWarmFloorRequest16Data> request16Listener)
        {
            _notifier = notifier;
            _reply03Listener = reply03Listener;
            _request16Listener = request16Listener;

            Reply03BytesTextVm = new AnyCommandPartViewModel();
            Request16BytesTextVm = new AnyCommandPartViewModel();

            MukWarmFloorSetParamsVm = new MukWarmFloorSetParamsViewModel(notifier, parameterSetter);

            _reply03Listener.DataReceived += Reply03ListenerOnDataReceived;
            _request16Listener.DataReceived += Request16ListenerOnDataReceived;
        }

        private void Reply03ListenerOnDataReceived(IList<byte> bytes, IMukWarmFloorReply03Data data)
        {
            _notifier.Notify(() =>
            {
                Reply03 = data;
                Reply03BytesTextVm.Update(bytes);
            });
        }

        private void Request16ListenerOnDataReceived(IList<byte> bytes, IMukWarmFloorRequest16Data data)
        {
            _notifier.Notify(() =>
            {
                var request16 = new MukWarmFloorRequest16BuilderFromBytes(bytes).Build();
                Request16 = request16;
                Request16BytesTextVm.Update(bytes);
            });
        }

        public IMukWarmFloorRequest16Data Request16
        {
            get => _request16;
            set
            {
                if (_request16 != value)
                {
                    _request16 = value;
                    RaisePropertyChanged("Request16");
                }
            }
        }

        public IMukWarmFloorReply03Data Reply03
        {
            get => _reply03;
            set
            {
                if (_reply03 != value)
                {
                    _reply03 = value;
                    RaisePropertyChanged("Reply03");
                }
            }
        }
    }
}
