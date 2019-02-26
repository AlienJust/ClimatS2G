using System;
using System.Collections.Generic;
using System.Timers;
using AlienJust.Support.Concurrent.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.TestSystems;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.TestSys
{
    internal class Freeze100Test : ITestSysAsync
    {
        private readonly IWorker<Action> _sharedTestsWorker;
        private readonly ICmdListener<IMukFanVaporizerDataReply03> _evaporator03Listener;
        private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _mukFlapWinterSummerReply03Listener;
        private readonly Timer _sharedTestTimer;
        private readonly IParameterSetter _parameterSetter;

        private readonly object _isTestRunningSync;
        private bool _isTestRunning;

        private readonly object _isTestCanceledSync;
        private bool _isTestCanceled;

        private readonly object _lastReceivedDataSyncMukFanVaporizer03Reply;
        private IMukFanVaporizerDataReply03 _lastReceivedDataMukFanVaporizer03Reply;

        private readonly object _lastReceivedDataSyncMukFlapWinterSummer03Reply;
        private IMukFlapWinterSummerReply03Telemetry _lastReceivedDataMukFlapWinterSummer03Reply;

        private readonly object _timeSync;
        private DateTime _beginTime;

        private readonly TimeSpan _testTimeout;

        private readonly object _testCompleteSync;
        private Action<TestSysResult> _testComplete;

        private readonly object _progressChangedSync;
        private Action<double, TestSysStepResult, string> _progressChanged;

        public Freeze100Test(IWorker<Action> sharedTestsWorker, ICmdListener<IMukFanVaporizerDataReply03> evaporator03Listener, ICmdListener<IMukFlapWinterSummerReply03Telemetry> mukFlapWinterSummerReply03Listener, Timer sharedTestTimer, IParameterSetter parameterSetter)
        {
            _sharedTestsWorker = sharedTestsWorker;

            _evaporator03Listener = evaporator03Listener;
            _mukFlapWinterSummerReply03Listener = mukFlapWinterSummerReply03Listener;

            _sharedTestTimer = sharedTestTimer;
            _parameterSetter = parameterSetter;

            _sharedTestTimer.Elapsed += SharedTestTimerOnElapsed;

            _evaporator03Listener.DataReceived += Evaporator03ListenerOnDataReceived;

            _isTestCanceledSync = new object();
            _isTestCanceled = false;

            _isTestRunningSync = new object();
            _isTestRunning = false;

            _timeSync = new object();
            _testTimeout = TimeSpan.FromSeconds(5.0);

            _lastReceivedDataSyncMukFanVaporizer03Reply = new object();
            _lastReceivedDataMukFanVaporizer03Reply = null;

            _testCompleteSync = new object();
            _testComplete = null;

            _progressChangedSync = new object();
            _progressChanged = null;
        }

        public DateTime BeginTime {
            get {
                lock (_timeSync) return _beginTime;
            }
            set {
                lock (_timeSync) _beginTime = value;
            }
        }

        public void BeginTest(Action testHasBegun, Action<double, TestSysStepResult, string> progressChanged, Action<TestSysResult> testComplete)
        {
            _sharedTestsWorker.AddWork(() =>
            {
                testHasBegun.Invoke();
                BeginTime = DateTime.Now;
                ProgressChanged = progressChanged;
                TestComplete = testComplete;
                IsTestRunning = true;

                _parameterSetter.SetParameterAsync(48, 1, Console.WriteLine);
                ProgressChanged.Invoke(100, TestSysStepResult.Good, "48 параметр КСМ установлен в режим Охлаждение 100%");
            });
        }

        private void SharedTestTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (IsTestRunning)
            {
                if (IsTestCanceled)
                {
                    IsTestRunning = false;
                    IsTestCanceled = true;
                    TestComplete.Invoke(TestSysResult.Canceled);
                }
                else if (e.SignalTime - BeginTime > _testTimeout)
                {
                    // timeout
                    IsTestRunning = false;
                    ProgressChanged.Invoke(100, TestSysStepResult.Bad, "МУК не включает Emerson");
                    TestComplete.Invoke(TestSysResult.Fail);
                }
                else
                {
                    if (LastReceivedData.Diagnostic3Parsed.MukIsSwitchingEmersionOn)
                    {
                        IsTestRunning = false;
                        LastReceivedData = null;
                        ProgressChanged.Invoke(100, TestSysStepResult.Good, "Включение Emerson осуществлено");
                        TestComplete.Invoke(TestSysResult.Success);
                    }
                }
            }
        }

        public Action<double, TestSysStepResult, string> ProgressChanged {
            get {
                lock (_progressChangedSync) return _progressChanged;
            }
            set {
                lock (_progressChangedSync) _progressChanged = value;
            }
        }

        public Action<TestSysResult> TestComplete {
            get {
                lock (_testCompleteSync) return _testComplete;
            }
            set {
                lock (_testCompleteSync) _testComplete = value;
            }
        }

        private void Evaporator03ListenerOnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data)
        {
            if (!IsTestCanceled)
            {
                lock (_lastReceivedDataSyncMukFanVaporizer03Reply) _lastReceivedDataMukFanVaporizer03Reply = data;
            }
        }

        public void CancelCurrentTest()
        {
            lock (_isTestCanceledSync) _isTestCanceled = true;
        }

        public bool IsTestCanceled {
            get {
                lock (_isTestCanceledSync) return _isTestCanceled;
            }
            set {
                lock (_isTestCanceledSync) _isTestCanceled = value;
            }
        }

        public bool IsTestRunning {
            get {
                lock (_isTestRunningSync) return _isTestRunning;
            }
            set {
                lock (_isTestRunningSync) _isTestRunning = value;
            }
        }

        public IMukFanVaporizerDataReply03 LastReceivedData {
            get {
                lock (_lastReceivedDataSyncMukFanVaporizer03Reply) return _lastReceivedDataMukFanVaporizer03Reply;
            }
            set {
                lock (_lastReceivedDataSyncMukFanVaporizer03Reply) _lastReceivedDataMukFanVaporizer03Reply = value;
            }
        }
    }
}
