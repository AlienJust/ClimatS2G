﻿using System.Collections.Generic;
using System.Globalization;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.SystemDiagnostic
{
    class SystemDiagCabinViewModel : ViewModelBase
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

        private const Colors HiVoltageOnLine = Colors.Red;
        private const Colors HiVoltageOffLine = Colors.Yellow;
        private const string HiVoltageOnLineText = "Есть!";
        private const string HiVoltageOffLineText = "Нет";
        private const string HiVoltageUnknownText = "??!";

        private readonly IThreadNotifier _uiNotifier;
        private readonly ICmdListener<IMukFlapAirReply03Telemetry> _cmdListenerMukFlapAirReply03;
        private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
        private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
        private readonly ICmdListener<IMukWarmFloorReply03Data> _cmdListenerMukWarmFloorReply03;

        private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm;
        private readonly ICmdListener<IBsSmRequest32Data> _cmdListenerBsSm32Request;
        private readonly ICmdListener<IBsSmReply32Data> _cmdListenerBsSm32Reply;
        private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvs1Reply65;


        private string _version;
        private string _workStage;

        private string _mukInfo2;
        private Colors _mukInfoColor2;

        private string _mukInfo3;
        private Colors _mukInfoColor3;

        private string _mukInfo5;
        private Colors _mukInfoColor5;


        private string _bsSmInfo;
        private Colors _bsSmInfoColor;

        private string _bvsInfo;
        private Colors _bvsInfoColor;

        private string _emersonInfo;
        private Colors _emersonInfoColor;

        private string _evaporatorFanControllerInfo;
        private Colors _evaporatorFanControllerInfoColor;

        private string _concentratorInfo;
        private Colors _concentratorInfoColor;


        private Colors _voltage380Color;
        private Colors _voltage3000Color;
        private string _voltage380Text;
        private string _voltage3000Text;


        private string _sensorOuterAirInfo;
        private Colors _sensorOuterAirInfoColor;

        private string _sensorRecycleAirInfo;
        private Colors _sensorRecycleAirInfoColor;

        private string _emersonPressure1;
        private Colors _emersonPressure1Color;

        private string _emersonTemperature1;
        private Colors _emersonTemperature1Color;

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
        public AutoViewModel AutoVm10 { get; }

        public BsSmFaultViewModel BsSmFaultVm1 { get; }
        public BsSmFaultViewModel BsSmFaultVm2 { get; }
        public BsSmFaultViewModel BsSmFaultVm3 { get; }
        public BsSmFaultViewModel BsSmFaultVm4 { get; }
        public BsSmFaultViewModel BsSmFaultVm5 { get; }

        public bool IsFullVersion { get; }
        public bool IsHalfOrFullVersion { get; }

        public SystemDiagCabinViewModel(
            bool isFullVersion,
            bool isHalfOrFullVersion,
            bool appAbilitiesIsHourCountersVisible,
            IThreadNotifier uiNotifier,
            CmdListenerBase<IMukFlapAirReply03Telemetry> cmdListenerMukFlapAirReply03,
            CmdListenerBase<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
            CmdListenerBase<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16,
            CmdListenerBase<IMukWarmFloorReply03Data> cmdListenerMukWarmFloorReply03,
            CmdListenerBase<IList<BytesPair>> cmdListenerKsmParams,
            CmdListenerBase<IBsSmRequest32Data> cmdListenerBsSm32Request,
            CmdListenerBase<IBsSmReply32Data> cmdListenerBsSm32Reply,
            CmdListenerBase<IBvsReply65Telemetry> cmdListenerBvsReply65)
        {
            IsFullVersion = isFullVersion;
            IsHalfOrFullVersion = isHalfOrFullVersion;

            _uiNotifier = uiNotifier;
            _cmdListenerMukFlapAirReply03 = cmdListenerMukFlapAirReply03;
            _cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
            _cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
            _cmdListenerMukWarmFloorReply03 = cmdListenerMukWarmFloorReply03;
            _cmdListenerKsm = cmdListenerKsmParams;
            _cmdListenerBsSm32Request = cmdListenerBsSm32Request;
            _cmdListenerBsSm32Reply = cmdListenerBsSm32Reply;
            _cmdListenerBvs1Reply65 = cmdListenerBvsReply65;


            _cmdListenerMukFlapAirReply03.DataReceived += CmdListenerMukFlapAirReply03OnDataReceived;
            _cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;
            _cmdListenerMukWarmFloorReply03.DataReceived += CmdListenerMukWarmFloorReply03OnDataReceived;
            _cmdListenerKsm.DataReceived += CmdListenerKsmOnDataReceived;
            _cmdListenerBsSm32Request.DataReceived += CmdListenerBsSm32RequestDataReceived;
            _cmdListenerBsSm32Reply.DataReceived += CmdListenerBsSm32ReplyDataReceived;
            _cmdListenerBvs1Reply65.DataReceived += CmdListenerBvs1Reply65OnDataReceived;

            ResetVmPropsToDefaultValues();
            AutoVm1 = new AutoViewModel("Контроль термостата предельной температуры в нагнетательной линии компрессора");
            AutoVm2 = new AutoViewModel("Контроль включения автомата компрессора к сети 380В");
            AutoVm3 = new AutoViewModel("Контроль включения автомата питания вентилятора конденсатора 110В");
            AutoVm4 = new AutoViewModel("Контроль включения автомата питания калорифера к сети 380В");
            AutoVm5 = new AutoViewModel("Контроль включения автомата питания пола к сети 380 в");
            AutoVm6 = new AutoViewModel("Контроль включения автомата питания вентилятора испарителя к сети 110В");
            AutoVm7 = new AutoViewModel("Контроль срабатывания термостата предельной температуры 120°С калорифера");

            AutoVm8 = new AutoViewModel("Контроль включения управляющего контактора подачи напряжения 380В на калорифер");
            AutoVm9 = new AutoViewModel("Контроль срабатывания контактора подачи 380В на обогрев пола");
            AutoVm10 = new AutoViewModel("Контроль срабатывания пускового контактора компрессора");

            BsSmFaultVm1 = new BsSmFaultViewModel();
            BsSmFaultVm2 = new BsSmFaultViewModel();
            BsSmFaultVm3 = new BsSmFaultViewModel();
            BsSmFaultVm4 = new BsSmFaultViewModel();
            BsSmFaultVm5 = new BsSmFaultViewModel();
        }

        private void CmdListenerBsSm32RequestDataReceived(IList<byte> bytes, IBsSmRequest32Data data)
        {
            _uiNotifier.Notify(() =>
            {
                BsSmFaultVm1.Code = data.Fault1;
                BsSmFaultVm2.Code = data.Fault2;
                BsSmFaultVm3.Code = data.Fault3;
                BsSmFaultVm4.Code = data.Fault4;
                BsSmFaultVm5.Code = data.Fault5;
            });
        }

        private void CmdListenerBsSm32ReplyDataReceived(IList<byte> bytes, IBsSmReply32Data data)
        {
            _uiNotifier.Notify(() =>
            {
                BsSmInfo = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.BsSmVersionNumber) : OkLinkText;
                BsSmInfoColor = OkLinkColor;

                if (data.WorkMode.HasVoltage3000V)
                {
                    Voltage3000Color = HiVoltageOnLine;
                    Voltage3000Text = HiVoltageOnLineText;
                }
                else
                {
                    Voltage3000Color = HiVoltageOffLine;
                    Voltage3000Text = HiVoltageOffLineText;
                }
            });
        }

        /// <summary>
        /// МУК заслонки наружного воздуха, MODBUS адрес = 2
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="data"></param>
        private void CmdListenerMukFlapAirReply03OnDataReceived(IList<byte> bytes, IMukFlapAirReply03Telemetry data)
        {
            _uiNotifier.Notify(() =>
            {
                MukInfo2 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber) : OkLinkText;
                MukInfoColor2 = OkLinkColor;

                if (data.Diagnostic1.NoEmersionControllerAnswer)
                {
                    EmersonInfo = NoLinkText;
                    EmersonInfoColor = NoLinkColor;

                    EmersonPressure1 = NoLinkText;
                    EmersonPressure1Color = NoLinkColor;
                    EmersonTemperature1 = NoLinkText;
                    EmersonTemperature1Color = NoLinkColor;
                }
                else
                {
                    EmersonInfo = OkLinkText;
                    EmersonInfoColor = OkLinkColor;

                    if (data.EmersonPressure.NoLinkWithSensor)
                    {
                        EmersonPressure1 = NoSensorText;
                        EmersonPressure1Color = NoSensorColor;
                    }
                    else
                    {
                        EmersonPressure1 = data.EmersonPressure.Indication.ToString("f2");
                        EmersonPressure1Color = OkSensorColor;
                    }

                    if (data.EmersonTemperature.NoLinkWithSensor)
                    {
                        EmersonTemperature1 = NoSensorText;
                        EmersonTemperature1Color = NoSensorColor;
                    }
                    else
                    {
                        EmersonTemperature1 = data.EmersonTemperature.Indication.ToString("f2");
                        EmersonTemperature1Color = OkSensorColor;
                    }
                }


                if (data.Diagnostic2.OsShowsFlapDoesNotReachLimitPositions)
                {
                    FlapAirOuterDiagInfo5 = ErDiagText;
                    FlapAirOuterDiagInfo5Color = ErDiagColor;
                }
                else
                {
                    FlapAirOuterDiagInfo5 = OkDiagText;
                    FlapAirOuterDiagInfo5Color = OkDiagColor;
                }

                if (data.Diagnostic2.OsShowsFlapDoesNotReach50Percent)
                {
                    FlapAirOuterDiagInfo6 = ErDiagText;
                    FlapAirOuterDiagInfo6Color = ErDiagColor;
                }
                else
                {
                    FlapAirOuterDiagInfo6 = OkDiagText;
                    FlapAirOuterDiagInfo6Color = OkDiagColor;
                }
            });
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

        /// <summary>
        /// Contactor control module for warm floor, MODBUS address = 5
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="data"></param>
        private void CmdListenerMukWarmFloorReply03OnDataReceived(IList<byte> bytes, IMukWarmFloorReply03Data data)
        {
            _uiNotifier.Notify(() =>
            {
                MukInfo5 = IsFullVersion ? new TextFormatterIntegerDotted().Format(data.FirmwareVersion) : OkLinkText;
                MukInfoColor5 = OkLinkColor;

                MukWarmFloorPwm = data.AnalogueInput.ToString();
                MukWarmFloorPwmColor = OkLinkColor;
            });
        }


        private void CmdListenerKsmOnDataReceived(IList<byte> bytes, IList<BytesPair> data)
        {
            _uiNotifier.Notify(() =>
            {
                Version = IsFullVersion ? new TextFormatterDotted(UnknownText).Format(data[27]) : OkLinkText;
                WorkStage = new TextFormatterWorkStage().Format(data[8]);

                // КСМ, бит "Нет связи с МУК заслонки наружного воздуха" взведен
                if (data[16].HighFirstUnsignedValue.GetBit(0))
                {
                    MukInfo2 = NoLinkText;
                    MukInfoColor2 = NoLinkColor;

                    FlapAirOuterDiagInfo5Color = NoLinkColor;
                    FlapAirOuterDiagInfo5 = NoLinkText;

                    FlapAirOuterDiagInfo6Color = NoLinkColor;
                    FlapAirOuterDiagInfo6 = NoLinkText;

                    // TODO: do I need check emerson here?
                    EmersonInfo = NoLinkText;
                    EmersonInfoColor = NoLinkColor;
                }

                // КСМ, бит "Нет связи с МУК вентилятора испарителя" взведен
                if (data[16].HighFirstUnsignedValue.GetBit(2))
                {
                    MukInfo3 = NoLinkText;
                    MukInfoColor3 = NoLinkColor;

                    EvaporatorFanControllerInfo = NoLinkText;
                    EvaporatorFanControllerInfoColor = NoLinkColor;

                    FanEvaporatorInfo = NoLinkText;
                    FanEvaporatorColor = NoLinkColor;

                    CalculatedTemperatureSetting = NoLinkText;
                }

                var siOuter = new SensorIndicationDoubleBasedOnBytesPair(data[0], 0.01, 0.00, new BytesPair(0x85, 0x00));
                if (siOuter.NoLinkWithSensor)
                {
                    SensorOuterAirInfo = NoSensorText;
                    SensorOuterAirInfoColor = NoSensorColor;
                }
                else
                {
                    SensorOuterAirInfo = siOuter.Indication.ToString("f2");
                    SensorOuterAirInfoColor = OkSensorColor;
                }

                var siRecycle = new SensorIndicationDoubleBasedOnBytesPair(data[4], 0.01, 0.00, new BytesPair(0x85, 0x00));
                if (siRecycle.NoLinkWithSensor)
                {
                    SensorRecycleAirInfo = NoSensorText;
                    SensorRecycleAirInfoColor = NoSensorColor;
                }
                else
                {
                    SensorRecycleAirInfo = siRecycle.Indication.ToString("f2");
                    SensorRecycleAirInfoColor = OkSensorColor;
                }

                // КСМ, бит "Нет связи с МУК тёплого пола" взведен
                if (data[16].HighFirstUnsignedValue.GetBit(6))
                {
                    MukInfo5 = NoLinkText;
                    MukInfoColor5 = NoLinkColor;

                    MukWarmFloorPwm = NoLinkText;
                    MukWarmFloorPwmColor = NoLinkColor;
                }
                else
                {
                    //MukInfo5 = OkLinkText;
                    MukInfoColor5 = OkLinkColor;
                }

                // BS-SM state.
                if (data[17].HighFirstUnsignedValue.GetBit(0))
                {
                    // TODO: Faults reset?
                    BsSmInfo = NoLinkText;
                    BsSmInfoColor = NoLinkColor;
                    Voltage3000Color = UnknownColor;
                    Voltage3000Text = HiVoltageUnknownText;
                }

                // BVS state.
                if (data[17].HighFirstUnsignedValue.GetBit(1))
                {
                    // TODO: AutoVm reset?
                    BvsInfo = NoLinkText;
                    BvsInfoColor = NoLinkColor;
                    Voltage380Color = UnknownColor;
                    Voltage380Text = HiVoltageUnknownText;

                    AutoVm1.IsOk = null;
                    AutoVm2.IsOk = null;
                    AutoVm3.IsOk = null;
                    AutoVm4.IsOk = null;
                    AutoVm5.IsOk = null;
                    AutoVm6.IsOk = null;
                    AutoVm7.IsOk = null;

                    AutoVm8.IsOk = null;
                    AutoVm9.IsOk = null;
                    AutoVm10.IsOk = null;
                }
                else
                {
                    BvsInfo = OkLinkText;
                    BvsInfoColor = OkLinkColor;
                }
            });
        }

        private void CmdListenerBvs1Reply65OnDataReceived(IList<byte> bytes, IBvsReply65Telemetry data)
        {
            _uiNotifier.Notify(() =>
            {
                BvsInfo = OkLinkText;
                BvsInfoColor = OkLinkColor;

                AutoVm1.IsOk = data.BvsInput4; // bit 1.3
                AutoVm2.IsOk = data.BvsInput9; // 2.0
                AutoVm3.IsOk = data.BvsInput10; // 2.1
                AutoVm4.IsOk = data.BvsInput11; // 2.2
                AutoVm5.IsOk = data.BvsInput12; // 2.3
                AutoVm6.IsOk = data.BvsInput13; // 2.4
                AutoVm7.IsOk = data.BvsInput14; // 2.5

                AutoVm8.IsOk = data.BvsInput7; // 1.6
                AutoVm9.IsOk = data.BvsInput8; // 1.7
                AutoVm10.IsOk = data.BvsInput6; // 1.5

                if (data.BvsInput1)
                {
                    Voltage380Color = HiVoltageOnLine;
                    Voltage380Text = HiVoltageOnLineText;
                }
                else
                {
                    Voltage380Color = HiVoltageOffLine;
                    Voltage380Text = HiVoltageOffLineText;
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


        public string MukInfo2
        {
            get => _mukInfo2;
            set
            {
                if (_mukInfo2 != value)
                {
                    _mukInfo2 = value;
                    RaisePropertyChanged(() => MukInfo2);
                }
            }
        }

        public Colors MukInfoColor2
        {
            get => _mukInfoColor2;
            set
            {
                if (_mukInfoColor2 != value)
                {
                    _mukInfoColor2 = value;
                    RaisePropertyChanged(() => MukInfoColor2);
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


        public string MukInfo5
        {
            get => _mukInfo5;
            set
            {
                if (_mukInfo5 != value)
                {
                    _mukInfo5 = value;
                    RaisePropertyChanged(() => MukInfo5);
                }
            }
        }

        public Colors MukInfoColor5
        {
            get => _mukInfoColor5;
            set
            {
                if (_mukInfoColor5 != value)
                {
                    _mukInfoColor5 = value;
                    RaisePropertyChanged(() => MukInfoColor5);
                }
            }
        }

        public string BsSmInfo
        {
            get => _bsSmInfo;
            set
            {
                if (_bsSmInfo != value)
                {
                    _bsSmInfo = value;
                    RaisePropertyChanged(() => BsSmInfo);
                }
            }
        }

        public Colors BsSmInfoColor
        {
            get => _bsSmInfoColor;
            set
            {
                if (_bsSmInfoColor != value)
                {
                    _bsSmInfoColor = value;
                    RaisePropertyChanged(() => BsSmInfoColor);
                }
            }
        }


        public string BvsInfo
        {
            get => _bvsInfo;
            set
            {
                if (_bvsInfo != value)
                {
                    _bvsInfo = value;
                    RaisePropertyChanged(() => BvsInfo);
                }
            }
        }

        public Colors BvsInfoColor
        {
            get => _bvsInfoColor;
            set
            {
                if (_bvsInfoColor != value)
                {
                    _bvsInfoColor = value;
                    RaisePropertyChanged(() => BvsInfoColor);
                }
            }
        }


        public string EmersonInfo
        {
            get => _emersonInfo;
            set
            {
                if (_emersonInfo != value)
                {
                    _emersonInfo = value;
                    RaisePropertyChanged(() => EmersonInfo);
                }
            }
        }

        public Colors EmersonInfoColor
        {
            get => _emersonInfoColor;
            set
            {
                if (_emersonInfoColor != value)
                {
                    _emersonInfoColor = value;
                    RaisePropertyChanged(() => EmersonInfoColor);
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

        public Colors Voltage380Color
        {
            get => _voltage380Color;
            set
            {
                if (_voltage380Color != value)
                {
                    _voltage380Color = value;
                    RaisePropertyChanged(() => Voltage380Color);
                }
            }
        }

        public Colors Voltage3000Color
        {
            get => _voltage3000Color;
            set
            {
                if (_voltage3000Color != value)
                {
                    _voltage3000Color = value;
                    RaisePropertyChanged(() => Voltage3000Color);
                }
            }
        }

        public string Voltage380Text
        {
            get => _voltage380Text;
            set
            {
                if (_voltage380Text != value)
                {
                    _voltage380Text = value;
                    RaisePropertyChanged(() => Voltage380Text);
                }
            }
        }

        public string Voltage3000Text
        {
            get => _voltage3000Text;
            set
            {
                if (_voltage3000Text != value)
                {
                    _voltage3000Text = value;
                    RaisePropertyChanged(() => Voltage3000Text);
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

            MukInfo2 = UnknownText;
            MukInfoColor2 = UnknownColor;

            MukInfo3 = UnknownText;
            MukInfoColor3 = UnknownColor;

            MukInfo5 = UnknownText;
            MukInfoColor5 = UnknownColor;

            BsSmInfo = UnknownText;
            BsSmInfoColor = UnknownColor;

            BvsInfo = UnknownText;
            BvsInfoColor = UnknownColor;

            EmersonInfo = UnknownText;
            EmersonInfoColor = UnknownColor;

            EvaporatorFanControllerInfo = UnknownText;
            EvaporatorFanControllerInfoColor = UnknownColor;

            ConcentratorInfo = UnknownText;
            ConcentratorInfoColor = UnknownColor;

            Voltage380Text = HiVoltageUnknownText;
            Voltage380Color = UnknownColor;
            Voltage3000Text = HiVoltageUnknownText;
            Voltage3000Color = UnknownColor;

            SensorOuterAirInfo = UnknownText;
            SensorOuterAirInfoColor = UnknownColor;

            SensorRecycleAirInfo = UnknownText;
            SensorRecycleAirInfoColor = UnknownColor;

            EmersonPressure1 = UnknownText;
            EmersonPressure1Color = UnknownColor;
            EmersonTemperature1 = UnknownText;
            EmersonTemperature1Color = UnknownColor;

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