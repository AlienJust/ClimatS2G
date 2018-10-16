﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.TestSystems {
	public sealed class TestSystemViewModel : ViewModelBase {
		private readonly IThreadNotifier _uiNotifier;
		private readonly DependedCommand _runTestAsync;
		private readonly DependedCommand _cancelTestAsync;
		private bool _isRunningTest;
		private Colors _testCompleteColor;

		public ITestSysAsync AssociatedWithModelTest { get; }
		public string TestName { get; }

		public ObservableCollection<LogLineColored> TestLogLines { get; }

		public TestSystemViewModel(IThreadNotifier uiNotifier, string testName, ITestSysAsync asyncTest, Colors testCompleteColor) {
			_uiNotifier = uiNotifier;
			TestName = testName;
			AssociatedWithModelTest = asyncTest;
			TestCompleteColor = testCompleteColor;


			_runTestAsync = new DependedCommand(RunTest, () => !IsRunningTest);
			_runTestAsync.AddDependOnProp(this, () => IsRunningTest);

			_cancelTestAsync = new DependedCommand(CancelTest, () => IsRunningTest);
			_cancelTestAsync.AddDependOnProp(this, () => IsRunningTest);

			TestLogLines = new ObservableCollection<LogLineColored>();
		}

		private void CancelTest() {
			AssociatedWithModelTest.CancelCurrentTest();
		}

		private void RunTest() {
			TestLogLines.Clear();
			AssociatedWithModelTest.BeginTest(() => {
				// TODO: test has been really started
				_uiNotifier.Notify(() => {
					IsRunningTest = true;
				});
			}, (progress, stepResult, message) => {
				_uiNotifier.Notify(() =>
				TestLogLines.Add(new LogLineColored(message, stepResult == TestSysStepResult.Good ? Colors.Lime : Colors.OrangeRed)));
			}
				, testResult => {
					_uiNotifier.Notify(() => {
						try {
							switch (testResult) {
								case TestSysResult.Success:
									TestCompleteColor = Colors.Lime;
									break;
								case TestSysResult.Fail:
									TestCompleteColor = Colors.OrangeRed;
									break;
								case TestSysResult.Canceled:
									TestCompleteColor = Colors.Yellow;
									break;
								default:
									throw new ArgumentOutOfRangeException(nameof(testResult), testResult, null);
							}
						}
						catch (Exception e) {
							Console.WriteLine(e);
						}
						finally {
							IsRunningTest = false;
						}
					});
				});
		}

		public bool IsRunningTest {
			get => _isRunningTest;
			set => SetProp(() => _isRunningTest != value, () => _isRunningTest = value, () => IsRunningTest);
		}


		public ICommand RunTestAsync => _runTestAsync;
		public ICommand CancelTestAsync => _cancelTestAsync;

		public Colors TestCompleteColor {
			get => _testCompleteColor;
			set {
				if (_testCompleteColor != value) {
					_testCompleteColor = value;
					RaisePropertyChanged(()=>TestCompleteColor);
				}
			}
		}
	}
}