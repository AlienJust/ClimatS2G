using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.Record {
	public class RecordViewModel : ViewModelBase, ICmdListenerStd {
		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;

		private readonly DependedCommand _startRecordCommand;
		private readonly DependedCommand _stopRecordCommand;
		private bool _isRecording;
		private readonly List<IList<byte>> _recordedData;

		public RecordViewModel(IThreadNotifier notifier, IWindowSystem windowSystem) {
			_notifier = notifier;
			_windowSystem = windowSystem;
			_isRecording = false;
			_recordedData = new List<IList<byte>>();
			_startRecordCommand = new DependedCommand(StartRecord, () => !IsRecording);
			_stopRecordCommand = new DependedCommand(StopRecord, () => IsRecording);
			_startRecordCommand.AddDependOnProp(this, () => IsRecording);
			_stopRecordCommand.AddDependOnProp(this, () => IsRecording);
		}

		private void StartRecord() {
			IsRecording = true;
			//_recordedData.Clear();
		}

		private void StopRecord() {
			IsRecording = false;
			if (_recordedData.Count > 0) {
				var filename = _windowSystem.ShowSaveFileDialog("Сохранение данных в виде текста", "Текстовые файлы|*.txt|Все файлы|*.*");
				if (!string.IsNullOrEmpty(filename)) {
					using (var sw = new StreamWriter(File.OpenWrite(filename))) { 
						for (int i = 0; i < _recordedData.Count; ++i) {
							var result = string.Empty;
							for (int j = 0; j < _recordedData[i].Count; ++j) {
								result += _recordedData[i][j].ToString("X2") + " ";
							}
							sw.WriteLine(result);
						}
						sw.Close();
					}
				}
				_recordedData.Clear();
			}
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			_notifier.Notify(() => {
				if (IsRecording) {
					_recordedData.Add(data);
					RaisePropertyChanged(() => RecordsCount);
				}
			});
		}

		public bool IsRecording {
			get => _isRecording;
			set {
				if (_isRecording != value) {
					_isRecording = value;
					RaisePropertyChanged(() => IsRecording);
				}
			}
		}
		public int RecordsCount => _recordedData.Count;
		public ICommand StartRecordCommand => _startRecordCommand;
		public ICommand StopRecordCommand => _stopRecordCommand;
	}
}