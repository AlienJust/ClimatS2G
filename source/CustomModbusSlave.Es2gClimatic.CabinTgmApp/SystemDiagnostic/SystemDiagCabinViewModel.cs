using System.Collections.Generic;
using System.Globalization;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;
using IMukFanVaporizerDataReply03 = CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03.IMukFanVaporizerDataReply03;
using IMukFanVaporizerDataRequest16 = CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Request16.IMukFanVaporizerDataRequest16;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.SystemDiagnostic
{
    class SystemDiagCabinTgmViewModel : ViewModelBase
    {
        private const string UnknownText = "Неизвестно";
        private const string NoLinkText = "Нет связи";
        private const string OkLinkText = "Есть связь";
        private const string NoSensorText = "Обрыв";

        private const Colors UnknownColor = Colors.Yellow;
        private const Colors NoLinkColor = Colors.Red;
        private const Colors NoSensorColor = Colors.Red;

        private const Colors OkLinkColor = Colors.LimeGreen;
        private const Colors OkSensorColor = Colors.LimeGreen;


        private const string OkDiagText = "Ok";
        private const string ErDiagText = "Er";
        private const Colors OkDiagColor = Colors.LimeGreen;
        private const Colors ErDiagColor = Colors.Red;


        private readonly IThreadNotifier _uiNotifier;
        
        private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
        private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
        private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;


        private string _version;
        private string _workStage;


        private string _mukInfo3;
        private Colors _mukInfoColor3;

        //private string _emersonInfo;
        //private Colors _emersonInfoColor;

        private string _evaporatorFanControllerInfo;
        private Colors _evaporatorFanControllerInfoColor;

        private string _concentratorInfo;
        private Colors _concentratorInfoColor;

        private string _sensorOuterAirInfo;
        private Colors _sensorOuterAirInfoColor;

        private string _sensorRecycleAirInfo;
        private Colors _sensorRecycleAirInfoColor;

        /// <summary>
        /// Датчик подаваемого воздуха
        /// </summary>
        private string _sensorSupplyAirInfo;

        private Colors _sensorSupplyAirInfoColor;

        private string _sensorInteriorAirInfo1;
        private Colors _sensorInteriorAirInfoColor1;

        private string _sensorInteriorAirInfo2;
        private Colors _sensorInteriorAirInfoColor2;

        private string _co2LevelText;
        private Colors _co2LevelColor;

        private string _emersonPressure1;
        private Colors _emersonPressure1Color;

        private string _emersonPressure2;
        private Colors _emersonPressure2Color;

        private string _emersonTemperature1;
        private Colors _emersonTemperature1Color;

        private string _emersonTemperature2;
        private Colors _emersonTemperature2Color;

        private string _flapAirOuterDiagInfo5;
        private Colors _flapAirOuterDiagInfo5Color;
        private string _flapAirOuterDiagInfo6;
        private Colors _flapAirOuterDiagInfo6Color;

        private string _fanEvaporatorInfo;
        private Colors _fanEvaporatorColor;
        
        private string _mukWarmFloorPwm;
        private Colors _mukWarmFloorPwmColor;

        private string _calculatedTemperatureSetting;

        public AutoViewModel AutoVm1 { get; }
        public AutoViewModel AutoVm2 { get; }
        public AutoViewModel AutoVm3 { get; }
        public AutoViewModel AutoVm4 { get; }
        public AutoViewModel AutoVm5 { get; }
        public AutoViewModel AutoVm6 { get; }
        public AutoViewModel AutoVm7 { get; }
        public AutoViewModel AutoVm8 { get; }
        public AutoViewModel AutoVm9 { get; }

        public BsSmFaultViewModel BsSmFaultVm1 { get; }
        public BsSmFaultViewModel BsSmFaultVm2 { get; }
        public BsSmFaultViewModel BsSmFaultVm3 { get; }
        public BsSmFaultViewModel BsSmFaultVm4 { get; }
        public BsSmFaultViewModel BsSmFaultVm5 { get; }

        public bool IsFullVersion { get; }
        public bool IsHalfOrFullVersion { get; }

        public SystemDiagCabinTgmViewModel(
            bool isFullVersion,
            bool isHalfOrFullVersion,
            bool appAbilitiesIsHourCountersVisible,
            IThreadNotifier uiNotifier,
            CmdListenerBase<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
            CmdListenerBase<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16,
            CmdListenerBase<IList<BytesPair>> cmdListenerKsmParams)
        {
            IsFullVersion = isFullVersion;
            IsHalfOrFullVersion = isHalfOrFullVersion;

            _uiNotifier = uiNotifier;
            _cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
            _cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
            _cmdListenerKsm = cmdListenerKsmParams;

            _cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;
            _cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
            _cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;

            ResetVmPropsToDefaultValues();
            AutoVm1 = new AutoViewModel("Автоматический выключатель приточного вентилятора 1");
            //AutoVm2 = new AutoViewModel("Автоматический выключатель приточного вентилятора 2");
            //AutoVm3 = new AutoViewModel("Автоматический выключатель вытяжных вентиляторов");
            AutoVm4 = new AutoViewModel("Автоматический выключатель Компрессора 1");
            //AutoVm5 = new AutoViewModel("Автоматический выключатель Компрессора 2");
            AutoVm6 = new AutoViewModel("Автоматический выключатель нагревателя 380");
            AutoVm7 = new AutoViewModel("Автоматический выключатель рециркуляционных нагревателей");
            AutoVm8 = new AutoViewModel("Автоматический выключатель вентилятора конденсатора");
            //AutoVm9 = new AutoViewModel("Автоматический выключатель обеззораживателя");

            BsSmFaultVm1 = new BsSmFaultViewModel();
            BsSmFaultVm2 = new BsSmFaultViewModel();
            BsSmFaultVm3 = new BsSmFaultViewModel();
            BsSmFaultVm4 = new BsSmFaultViewModel();
            BsSmFaultVm5 = new BsSmFaultViewModel();
        }


        /// <summary>
        /// МУК вентилятора испарителя, MODBUS адрес = 3
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="data"></param>
        private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data)
        {
            _uiNotifier.Notify(() =>
            {
                MukInfo3 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
                MukInfoColor3 = OkLinkColor;

                if (data.Diagnostic1Parsed.FanControllerLinkLost)
                {
                    EvaporatorFanControllerInfo = NoLinkText;
                    EvaporatorFanControllerInfoColor = NoLinkColor;
                }
                else
                {
                    EvaporatorFanControllerInfo = OkLinkText;
                    EvaporatorFanControllerInfoColor = OkLinkColor;
                }

                if (data.TemperatureAddress1.NoLinkWithSensor)
                {
                    SensorOuterAirInfo = NoSensorText;
                    SensorOuterAirInfoColor = NoSensorColor;
                }
                else
                {
                    SensorOuterAirInfo = data.TemperatureAddress1.Indication.ToString("f2");
                    SensorOuterAirInfoColor = OkSensorColor;
                }

                if (data.TemperatureAddress2.NoLinkWithSensor)
                {
                    SensorRecycleAirInfo = NoSensorText;
                    SensorRecycleAirInfoColor = NoSensorColor;
                }
                else
                {
                    SensorRecycleAirInfo = data.TemperatureAddress2.Indication.ToString("f2");
                    SensorRecycleAirInfoColor = OkSensorColor;
                }

                if (data.TemperatureAddress3.NoLinkWithSensor)
                {
                    SensorSupplyAirInfo = NoSensorText;
                    SensorSupplyAirInfoColor = NoSensorColor;
                }
                else
                {
                    SensorSupplyAirInfo = data.TemperatureAddress3.Indication.ToString("f2");
                    SensorSupplyAirInfoColor = OkSensorColor;
                }

                FanEvaporatorInfo = data.FanSpeed.ToString(CultureInfo.InvariantCulture);
                if (data.Diagnostic1.GetBit(4))
                {
                    FanEvaporatorColor = ErDiagColor;
                    FanEvaporatorInfo += ", неисправность";
                }
                else
                {
                    FanEvaporatorColor = OkDiagColor;
                    FanEvaporatorInfo += ", норма";
                }

                CalculatedTemperatureSetting = data.CalculatedTemperatureSetting.ToString("f2");
            });
        }


        private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data)
        {
            _uiNotifier.Notify(() =>
            {
                Version = IsFullVersion ? new TextFormatterDotted(UnknownText).Format(data[34]) : OkLinkText;
                WorkStage = new TextFormatterWorkStage().Format(data[8]);

                // КСМ, бит "Нет связи с МУК вентилятора испарителя" взведен
                if (data[16].HighFirstUnsignedValue.GetBit(2))
                {
                    MukInfo3 = NoLinkText;
                    MukInfoColor3 = NoLinkColor;

                    EvaporatorFanControllerInfo = NoLinkText;
                    EvaporatorFanControllerInfoColor = NoLinkColor;

                    SensorOuterAirInfo = NoLinkText;
                    SensorOuterAirInfoColor = NoLinkColor;

                    SensorRecycleAirInfo = NoLinkText;
                    SensorRecycleAirInfoColor = NoLinkColor;

                    SensorSupplyAirInfo = NoLinkText;
                    SensorSupplyAirInfoColor = NoLinkColor;

                    FanEvaporatorInfo = NoLinkText;
                    FanEvaporatorColor = NoLinkColor;

                    CalculatedTemperatureSetting = NoLinkText;
                }

                // КСМ, бит "Нет связи с МУК тёплого " взведен
                if (data[16].HighFirstUnsignedValue.GetBit(6))
                {
                }


                var oneWireSensor1 = new SensorIndicationDoubleBasedOnBytesPair(data[0], 0.01, 0.0, new BytesPair(0x85, 0x00));
                if (oneWireSensor1.NoLinkWithSensor)
                {
                    SensorInteriorAirInfo1 = NoSensorText;
                    SensorInteriorAirInfoColor1 = NoSensorColor;
                }
                else
                {
                    SensorInteriorAirInfo1 = oneWireSensor1.Indication.ToString("f2");
                    SensorInteriorAirInfoColor1 = OkSensorColor;
                }

                var oneWireSensor2 = new SensorIndicationDoubleBasedOnBytesPair(data[1], 0.01, 0.0, new BytesPair(0x85, 0x00));
                if (oneWireSensor2.NoLinkWithSensor)
                {
                    SensorInteriorAirInfo2 = NoSensorText;
                    SensorInteriorAirInfoColor2 = NoSensorColor;
                }
                else
                {
                    SensorInteriorAirInfo2 = oneWireSensor2.Indication.ToString("f2");
                    SensorInteriorAirInfoColor2 = OkSensorColor;
                }
            });
        }

        private void CmdListenerMukVaporizerRequest16DataReceived(IList<byte> bytes, IMukFanVaporizerDataRequest16 data)
        {
            _uiNotifier.Notify(() =>
            {
                if (data != null)
                {
                    
                }
            });
        }

       
        public string Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    RaisePropertyChanged(() => Version);
                }
            }
        }

        public string WorkStage
        {
            get => _workStage;
            set
            {
                if (_workStage != value)
                {
                    _workStage = value;
                    RaisePropertyChanged(() => WorkStage);
                }
            }
        }


        public string MukInfo3
        {
            get => _mukInfo3;
            set
            {
                if (_mukInfo3 != value)
                {
                    _mukInfo3 = value;
                    RaisePropertyChanged(() => MukInfo3);
                }
            }
        }

        public Colors MukInfoColor3
        {
            get => _mukInfoColor3;
            set
            {
                if (_mukInfoColor3 != value)
                {
                    _mukInfoColor3 = value;
                    RaisePropertyChanged(() => MukInfoColor3);
                }
            }
        }


        
        public string EvaporatorFanControllerInfo
        {
            get => _evaporatorFanControllerInfo;
            set
            {
                if (_evaporatorFanControllerInfo != value)
                {
                    _evaporatorFanControllerInfo = value;
                    RaisePropertyChanged(() => EvaporatorFanControllerInfo);
                }
            }
        }

        public Colors EvaporatorFanControllerInfoColor
        {
            get => _evaporatorFanControllerInfoColor;
            set
            {
                if (_evaporatorFanControllerInfoColor != value)
                {
                    _evaporatorFanControllerInfoColor = value;
                    RaisePropertyChanged(() => EvaporatorFanControllerInfoColor);
                }
            }
        }

        public string ConcentratorInfo
        {
            get => _concentratorInfo;
            set
            {
                if (_concentratorInfo != value)
                {
                    _concentratorInfo = value;
                    RaisePropertyChanged(() => ConcentratorInfo);
                }
            }
        }

        public Colors ConcentratorInfoColor
        {
            get => _concentratorInfoColor;
            set
            {
                if (_concentratorInfoColor != value)
                {
                    _concentratorInfoColor = value;
                    RaisePropertyChanged(() => ConcentratorInfoColor);
                }
            }
        }

        
        public string SensorOuterAirInfo
        {
            get => _sensorOuterAirInfo;
            set
            {
                if (_sensorOuterAirInfo != value)
                {
                    _sensorOuterAirInfo = value;
                    RaisePropertyChanged(() => SensorOuterAirInfo);
                }
            }
        }

        public Colors SensorOuterAirInfoColor
        {
            get => _sensorOuterAirInfoColor;
            set
            {
                if (_sensorOuterAirInfoColor != value)
                {
                    _sensorOuterAirInfoColor = value;
                    RaisePropertyChanged(() => SensorOuterAirInfoColor);
                }
            }
        }


        public string SensorRecycleAirInfo
        {
            get => _sensorRecycleAirInfo;
            set
            {
                if (_sensorRecycleAirInfo != value)
                {
                    _sensorRecycleAirInfo = value;
                    RaisePropertyChanged(() => SensorRecycleAirInfo);
                }
            }
        }

        public Colors SensorRecycleAirInfoColor
        {
            get => _sensorRecycleAirInfoColor;
            set
            {
                if (_sensorRecycleAirInfoColor != value)
                {
                    _sensorRecycleAirInfoColor = value;
                    RaisePropertyChanged(() => SensorRecycleAirInfoColor);
                }
            }
        }

        #region Props Датчик подаваемого воздуха

        public string SensorSupplyAirInfo
        {
            get => _sensorSupplyAirInfo;
            set
            {
                if (_sensorSupplyAirInfo != value)
                {
                    _sensorSupplyAirInfo = value;
                    RaisePropertyChanged(() => SensorSupplyAirInfo);
                }
            }
        }

        public Colors SensorSupplyAirInfoColor
        {
            get => _sensorSupplyAirInfoColor;
            set
            {
                if (_sensorSupplyAirInfoColor != value)
                {
                    _sensorSupplyAirInfoColor = value;
                    RaisePropertyChanged(() => SensorSupplyAirInfoColor);
                }
            }
        }

        #endregion

        public string SensorInteriorAirInfo1
        {
            get => _sensorInteriorAirInfo1;
            set
            {
                if (_sensorInteriorAirInfo1 != value)
                {
                    _sensorInteriorAirInfo1 = value;
                    RaisePropertyChanged(() => SensorInteriorAirInfo1);
                }
            }
        }

        public Colors SensorInteriorAirInfoColor1
        {
            get => _sensorInteriorAirInfoColor1;
            set
            {
                if (_sensorInteriorAirInfoColor1 != value)
                {
                    _sensorInteriorAirInfoColor1 = value;
                    RaisePropertyChanged(() => SensorInteriorAirInfoColor1);
                }
            }
        }


        public string SensorInteriorAirInfo2
        {
            get => _sensorInteriorAirInfo2;
            set
            {
                if (_sensorInteriorAirInfo2 != value)
                {
                    _sensorInteriorAirInfo2 = value;
                    RaisePropertyChanged(() => SensorInteriorAirInfo2);
                }
            }
        }

        public Colors SensorInteriorAirInfoColor2
        {
            get => _sensorInteriorAirInfoColor2;
            set
            {
                if (_sensorInteriorAirInfoColor2 != value)
                {
                    _sensorInteriorAirInfoColor2 = value;
                    RaisePropertyChanged(() => SensorInteriorAirInfoColor2);
                }
            }
        }


        public string Co2LevelText
        {
            get => _co2LevelText;
            set
            {
                if (value != "2500" && _co2LevelText != value)
                {
                    _co2LevelText = value;
                    RaisePropertyChanged(() => Co2LevelText);
                }
            }
        }

        public Colors Co2LevelColor
        {
            get => _co2LevelColor;
            set
            {
                if (_co2LevelColor != value)
                {
                    _co2LevelColor = value;
                    RaisePropertyChanged(() => Co2LevelColor);
                }
            }
        }


        public string EmersonPressure1
        {
            get => _emersonPressure1;
            set
            {
                if (_emersonPressure1 != value)
                {
                    _emersonPressure1 = value;
                    RaisePropertyChanged(() => EmersonPressure1);
                }
            }
        }

        public Colors EmersonPressure1Color
        {
            get => _emersonPressure1Color;
            set
            {
                if (_emersonPressure1Color != value)
                {
                    _emersonPressure1Color = value;
                    RaisePropertyChanged(() => EmersonPressure1Color);
                }
            }
        }


        public string EmersonPressure2
        {
            get => _emersonPressure2;
            set
            {
                if (_emersonPressure2 != value)
                {
                    _emersonPressure2 = value;
                    RaisePropertyChanged(() => EmersonPressure2);
                }
            }
        }

        public Colors EmersonPressure2Color
        {
            get => _emersonPressure2Color;
            set
            {
                if (_emersonPressure2Color != value)
                {
                    _emersonPressure2Color = value;
                    RaisePropertyChanged(() => EmersonPressure2Color);
                }
            }
        }


        public string EmersonTemperature1
        {
            get => _emersonTemperature1;
            set
            {
                if (_emersonTemperature1 != value)
                {
                    _emersonTemperature1 = value;
                    RaisePropertyChanged(() => EmersonTemperature1);
                }
            }
        }

        public Colors EmersonTemperature1Color
        {
            get => _emersonTemperature1Color;
            set
            {
                if (_emersonTemperature1Color != value)
                {
                    _emersonTemperature1Color = value;
                    RaisePropertyChanged(() => EmersonTemperature1Color);
                }
            }
        }

        public string EmersonTemperature2
        {
            get => _emersonTemperature2;
            set
            {
                if (_emersonTemperature2 != value)
                {
                    _emersonTemperature2 = value;
                    RaisePropertyChanged(() => EmersonTemperature2);
                }
            }
        }

        public Colors EmersonTemperature2Color
        {
            get => _emersonTemperature2Color;
            set
            {
                if (_emersonTemperature2Color != value)
                {
                    _emersonTemperature2Color = value;
                    RaisePropertyChanged(() => EmersonTemperature2Color);
                }
            }
        }



        public string FlapAirOuterDiagInfo5
        {
            get => _flapAirOuterDiagInfo5;
            set
            {
                if (_flapAirOuterDiagInfo5 != value)
                {
                    _flapAirOuterDiagInfo5 = value;
                    RaisePropertyChanged(() => FlapAirOuterDiagInfo5);
                }
            }
        }

        public Colors FlapAirOuterDiagInfo5Color
        {
            get => _flapAirOuterDiagInfo5Color;
            set
            {
                if (_flapAirOuterDiagInfo5Color != value)
                {
                    _flapAirOuterDiagInfo5Color = value;
                    RaisePropertyChanged(() => FlapAirOuterDiagInfo5Color);
                }
            }
        }

        public string FlapAirOuterDiagInfo6
        {
            get => _flapAirOuterDiagInfo6;
            set
            {
                if (_flapAirOuterDiagInfo6 != value)
                {
                    _flapAirOuterDiagInfo6 = value;
                    RaisePropertyChanged(() => FlapAirOuterDiagInfo6);
                }
            }
        }

        public Colors FlapAirOuterDiagInfo6Color
        {
            get => _flapAirOuterDiagInfo6Color;
            set
            {
                if (_flapAirOuterDiagInfo6Color != value)
                {
                    _flapAirOuterDiagInfo6Color = value;
                    RaisePropertyChanged(() => FlapAirOuterDiagInfo6Color);
                }
            }
        }


        public Colors MukWarmFloorPwmColor
        {
            get => _mukWarmFloorPwmColor;
            set
            {
                if (_mukWarmFloorPwmColor != value)
                {
                    _mukWarmFloorPwmColor = value;
                    RaisePropertyChanged(() => MukWarmFloorPwmColor);
                }
            }
        }

        public string MukWarmFloorPwm
        {
            get => _mukWarmFloorPwm;
            set
            {
                if (_mukWarmFloorPwm != value)
                {
                    _mukWarmFloorPwm = value;
                    RaisePropertyChanged(() => MukWarmFloorPwm);
                }
            }
        }

        public Colors FanEvaporatorColor
        {
            get => _fanEvaporatorColor;
            set
            {
                if (_fanEvaporatorColor != value)
                {
                    _fanEvaporatorColor = value;
                    RaisePropertyChanged(() => FanEvaporatorColor);
                }
            }
        }

        public string FanEvaporatorInfo
        {
            get => _fanEvaporatorInfo;
            set
            {
                if (_fanEvaporatorInfo != value)
                {
                    _fanEvaporatorInfo = value;
                    RaisePropertyChanged(() => FanEvaporatorInfo);
                }
            }
        }

        public string CalculatedTemperatureSetting
        {
            get => _calculatedTemperatureSetting;
            set
            {
                if (_calculatedTemperatureSetting != value)
                {
                    _calculatedTemperatureSetting = value;
                    RaisePropertyChanged(() => CalculatedTemperatureSetting);
                }
            }
        }

        void ResetVmPropsToDefaultValues()
        {
            Version = UnknownText;
            WorkStage = UnknownText;

            MukInfo3 = UnknownText;
            MukInfoColor3 = UnknownColor;


            EvaporatorFanControllerInfo = UnknownText;
            EvaporatorFanControllerInfoColor = UnknownColor;

            ConcentratorInfo = UnknownText;
            ConcentratorInfoColor = UnknownColor;


            SensorOuterAirInfo = UnknownText;
            SensorOuterAirInfoColor = UnknownColor;

            SensorRecycleAirInfo = UnknownText;
            SensorRecycleAirInfoColor = UnknownColor;

            SensorSupplyAirInfo = UnknownText;
            SensorSupplyAirInfoColor = UnknownColor;

            SensorInteriorAirInfo1 = UnknownText;
            SensorInteriorAirInfoColor1 = UnknownColor;

            SensorInteriorAirInfo2 = UnknownText;
            SensorInteriorAirInfoColor2 = UnknownColor;

            Co2LevelText = UnknownText;
            Co2LevelColor = UnknownColor;


            EmersonPressure1 = UnknownText;
            EmersonPressure1Color = UnknownColor;
            EmersonPressure2 = UnknownText;
            EmersonPressure2Color = UnknownColor;
            EmersonTemperature1 = UnknownText;
            EmersonTemperature1Color = UnknownColor;
            EmersonTemperature2 = UnknownText;
            EmersonTemperature2Color = UnknownColor;

            FlapAirOuterDiagInfo5 = UnknownText;
            FlapAirOuterDiagInfo5Color = UnknownColor;
            FlapAirOuterDiagInfo6 = UnknownText;
            FlapAirOuterDiagInfo6Color = UnknownColor;

            MukWarmFloorPwm = UnknownText;
            MukWarmFloorPwmColor = UnknownColor;

            FanEvaporatorInfo = UnknownText;
            FanEvaporatorColor = UnknownColor;

            CalculatedTemperatureSetting = UnknownText;
        }
    }
}