using System;
using System.Collections.Concurrent;
using AlienJust.Support.Numeric;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbus.Slave.FastReply.Queued
{
    public class ReplyGeneratorWithQueueAttempted : IParameterSetter, IFastReplyGenerator, IFastReplyAcceptor
    {
        // адрес, значение, callback, число попыток
        private readonly BlockingCollection<QueueItem> _itemsToWrite;
        private readonly byte[] _noAnyParamWasChangedReplyToBeFast;
        private readonly byte[] _fastReply;

        private const int MaxAttemptsCount = 4;

        private readonly Exception _commonExceptionToBeFast;

        private QueueItem _currentItem;
        private readonly object _currentItemSyncLockObject;

        public ReplyGeneratorWithQueueAttempted()
        {
            _itemsToWrite = new BlockingCollection<QueueItem>(new ConcurrentQueue<QueueItem>());

            _noAnyParamWasChangedReplyToBeFast = new byte[] { 20, 33, 0, 0, 0, 0, 0x08, 0xBF }; // default reply with precalculated CRC16
            _fastReply = new byte[] { 20, 33, 0, 0, 0, 0, 0, 0 }; // reply pattern

            _commonExceptionToBeFast = new Exception("За указанное число попыток нужный номер параметра не был получен");

            _currentItem = null;
            _currentItemSyncLockObject = new object();
        }

        // called when need to answer reply:
        public byte[] GenerateReply()
        {
            lock (_currentItemSyncLockObject)
            {
                if (_currentItem == null)
                {
                    bool takeResult = _itemsToWrite.TryTake(out _currentItem);

                    // default answer (zeroes):
                    if (!takeResult)
                    {
                        return _noAnyParamWasChangedReplyToBeFast;
                    }
                }

                _fastReply[2] = (byte)((_currentItem.ParameterIndex & 0xFF00) >> 8);
                _fastReply[3] = (byte)(_currentItem.ParameterIndex & 0xFF);

                _fastReply[4] = (byte)((_currentItem.ParameterValue & 0xFF00) >> 8);
                _fastReply[5] = (byte)(_currentItem.ParameterValue & 0xFF);

                // TODO: form reply, based on param index and
                MathExtensions.FillCrc16AtTheEndOfArrayHighLow(_fastReply);
                return _fastReply;
            }
        }

        // called from UI by user:
        public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback)
        {
            lock (_currentItemSyncLockObject)
            {
                /*if (_currentItem != null) {
					_currentItem.OnCompleteCallback.Invoke(new Exception("Не удалось записать параметр, т.к. поступил запрос на запись нового параметра"));
					_currentItem = null;
				}*/

                _itemsToWrite.Add(new QueueItem { ParameterIndex = zeroBasedParameterNumber, ParameterValue = value, OnCompleteCallback = callback, AttemptsCount = MaxAttemptsCount });
            }
        }

        // called when reply accepted:
        public void AcceptReply(byte[] replyBytes)
        {
            lock (_currentItemSyncLockObject)
            {
                if (_currentItem != null)
                {
                    int parameterIndexInReply = replyBytes[2] * 256 + replyBytes[3];
                    ushort parameterValueInReply = (ushort)(replyBytes[4] * 256 + replyBytes[5]);

                    if (_currentItem.ParameterIndex == parameterIndexInReply && _currentItem.ParameterValue == parameterValueInReply)
                    {
                        _currentItem.OnCompleteCallback.Invoke(null);
                        _currentItem = null;
                    }
                    else
                    {
                        // если попытки кончились:
                        if (_currentItem.AttemptsCount <= 0)
                        {
                            _currentItem.OnCompleteCallback.Invoke(_commonExceptionToBeFast);
                            _currentItem = null;
                        }
                        else _currentItem.AttemptsCount--;
                    }
                }
            }
        }
    }
}
