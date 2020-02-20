using System;
using System.Collections.Generic;
using System.Threading;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave
{
    public class SerialChannel : ISerialChannel, ICommandPartFoundListener, ISerialChannelWithIoProgress
    {
        //public readonly ILoggerWithStackTrace _logger;
        public event CommandHearedDelegate CommandHeared;
        public event CommandHearedWithReplyPossibilityDelegate CommandHearedWithReplyPossibility;

        private readonly SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action> _backgroundWorker;
        private readonly IWorker<Action> _readDataScheduler;
        private readonly IWorker<Action> _notifyWorker;
        private readonly List<byte> _incomingBuffer;

        //private readonly ISerialPortContainer _portContainer;
        private ISerialPortContainer _portContainer;
        private ISendAbility _sendAbility;
        private readonly ICommandPartSearcher _commandPartSearcher;

        private bool _dontStop;
        private readonly object _dontStopSync;
        private ISerialPortContainerWithProgress _portContainerWithProgress;

        public SerialChannel(ICommandPartSearcher commandPartSearcher/*, ILoggerWithStackTrace logger*/)
        {
            _commandPartSearcher = commandPartSearcher;
            //_portContainer = portContainer;
            //_sendAbility = sendAbility;
            //_logger = logger;

            _dontStop = true;
            _dontStopSync = new object();

            _incomingBuffer = new List<byte>();


            _backgroundWorker = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("SerialBackWorker", a => a(), ThreadPriority.BelowNormal, true, null);
            _readDataScheduler = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("SerialDataReadScheduler", a => a(), ThreadPriority.BelowNormal, true, null);
            _notifyWorker = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("NotifyWorker", a => a(), ThreadPriority.BelowNormal, true, null);

            ScheduleReadDataInBackground();
        }

        public void SelectPortAsync(ISerialPortContainer portContainer, Action<Exception> comPortOpenedCallbackAction)
        {
            //_logger.Log("Закрытие ранее открытого порта (если был открыт) и открытие нового: " + portContainer, null);
            _backgroundWorker.AddWork(() =>
            {
                Exception exception;
                try
                {
                    if (_portContainer != null && _portContainer.IsOpen)
                        ClosePortSyncUnsafe();

                    _portContainer = portContainer;
                    _portContainer.Open();
                    _sendAbility = portContainer;

                    _portContainerWithProgress = _portContainer as ISerialPortContainerWithProgress;

                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (comPortOpenedCallbackAction != null)
                {
                    _notifyWorker.AddWork(() => comPortOpenedCallbackAction(exception));
                }
            });
        }

        public void CloseCurrentPortAsync(Action<Exception> comPortClosedCallbackAction)
        {
            _backgroundWorker.AddWork(() =>
            {
                Exception exception;
                try
                {
                    ClosePortSyncUnsafe();
                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (comPortClosedCallbackAction != null)
                {
                    _notifyWorker.AddWork(() => comPortClosedCallbackAction(exception));
                }
            });
        }

        private void ClosePortSyncUnsafe()
        {
            _portContainer.Close();
            _incomingBuffer.Clear();
        }

        private void ScheduleReadDataInBackground()
        {
            _readDataScheduler.AddWork(() =>
            {
                while (true)
                {
                    var waiter = new AutoResetEvent(false);

                    _backgroundWorker.AddWork(() =>
                    {
                        try
                        {
                            //_logger.Log("Reading bytes from port (8 bytes, timeout 1 second)...");
                            var bytes = _portContainer.Read(8, TimeSpan.FromSeconds(1));
                            //_logger.Log("Readed bytes from port: " + bytes.ToText());
                            _incomingBuffer.AddRange(bytes);
                            //_logger.Log("INCOMING BUFFER BYTES COUNT AFTER READ = " + _incomingBuffer.Count);

                            AnalyzeIncomingBuffer(); // analyzing in io thread to have possibility to reply
                        }
                        catch /*(Exception ex)*/
                        {
                            //_logger.Log(ex);
                            return;
                        }
                        finally
                        {
                            waiter.Set();
                        }
                    });
                    // back to read data scheduler thread:
                    waiter.WaitOne();

                    lock (_dontStopSync)
                    {
                        if (!_dontStop)
                            return;
                    }
                }
            });
        }

        private void AnalyzeIncomingBuffer()
        {
            _commandPartSearcher.SearchForCommands(_incomingBuffer, this);
        }

        public void CommandPartFound(ICommandPart commandPart)
        {
            // ultra fast synchronious notification with possibility to reply:
            var commandHearedFast = CommandHearedWithReplyPossibility;
            commandHearedFast?.Invoke(commandPart, _sendAbility);

            // slow async notification
            _notifyWorker.AddWork(() =>
            {
                try
                {
                    var commandHeared = CommandHeared;
                    commandHeared?.Invoke(commandPart);

                    // TODO: Sync
                    if (_portContainerWithProgress != null)
                    {
                        var progressChanged = ProgressChanged;
                        progressChanged?.Invoke(_portContainerWithProgress.ProgressPercentage);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.Log(ex, null);
                }
            });
        }

        // calls at the end of the program:
        public void StopWork()
        {
            _backgroundWorker.StopAsync();
            _backgroundWorker.WaitStopComplete();

            string result = "-----------------------------------------------------------------------------------------" + Environment.NewLine;
            for (int i = 0; i < _incomingBuffer.Count; ++i)
            {
                result += _incomingBuffer[i].ToString("X2") + " ";
                if (i % 16 == 0)
                    result += Environment.NewLine;
                else if (i % 8 == 0)
                    result += "    ";
                else if (i % 4 == 0)
                    result += "  ";
            }

            //_logger.Log(result, null);
            lock (_dontStopSync)
                _dontStop = false;
        }

        public event ProgressChangedDelegate ProgressChanged;
    }
}