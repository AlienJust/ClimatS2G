using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Numeric;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp {
	class ReplyGeneratorWithQueue : IParameterSetter IReplyGenerator, IReplyAcceptor {
		private readonly IThreadNotifier _uiNotifier;
		private readonly BlockingCollection<Tuple<int, ushort, Action<Exception>>> _itemsToWrite;
		private readonly byte[] _noAnyParamWasChangedReplyToBeFast;
		private readonly byte[] _fastReply;

		private readonly Exception _commonExceptionToBeFast;

		private Tuple<int, ushort, Action<Exception>> _currentItem;

		public ReplyGeneratorWithQueue(IThreadNotifier uiNotifier) {
			_uiNotifier = uiNotifier;
			_itemsToWrite = new BlockingCollection<Tuple<int, ushort, Action<Exception>>>(new ConcurrentQueue<Tuple<int, ushort, Action<Exception>>>());

			_noAnyParamWasChangedReplyToBeFast = new byte[] {20, 33, 0, 0, 0, 0, 0, 0}; // default reply
			_fastReply = new byte[] { 20, 33, 0, 0, 0, 0, 0, 0 }; // reply pattern

			_commonExceptionToBeFast = new Exception("В ближайшем ответе получен неверный номер параметра");

			_currentItem = null;
		}

		public byte[] GenerateReply() {
			if (_itemsToWrite.Count == 0) {
				_currentItem = null;
				return _noAnyParamWasChangedReplyToBeFast;
			}

			_currentItem = _itemsToWrite.Take();
			// TODO: form reply, based on param index and
			_fastReply[2] = (byte)((_currentItem.Item1 & 0xFF00) >> 8);
			_fastReply[3] = (byte)(_currentItem.Item1 & 0xFF);

			_fastReply[4] = (byte)((_currentItem.Item1 & 0xFF00) >> 8);
			_fastReply[5] = (byte)(_currentItem.Item1 & 0xFF);

			AlienJust.Support.Numeric.MathExtensions.

			
			return _fastReply;
		}

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			_itemsToWrite.Add(new Tuple<int, ushort, Action<Exception>>(zeroBasedParameterNumber, value, callback));
		}

		public void AcceptReply(byte[] replyBytes) {
			if (_currentItem != null) {
				ushort parameterIndexInReply = 0; // TODO: extract from reply
				if (_currentItem.Item1 == parameterIndexInReply) {
					_currentItem.Item3.Invoke(null);
				}
				else _currentItem.Item3.Invoke(_commonExceptionToBeFast);
			}
		}
	}

	interface IFastReplyGenerator {
		byte[] GenerateReply();
	}

	interface IFastReplyAcceptor {
		void AcceptReply(byte[] replyBytes);
	}

}
