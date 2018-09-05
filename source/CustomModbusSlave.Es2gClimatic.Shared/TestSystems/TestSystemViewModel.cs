using System;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.TestSystems {
	public sealed class TestSystemViewModel : ViewModelBase {
		private readonly IThreadNotifier _uiNotifier;
		private readonly DependedCommand _runTestAsync;
		private readonly DependedCommand _cancelTestAsync;
		private bool _isRunningTest;

		public ITestSysAsync AssociatedWithModelTest { get; }
		public string TestName { get; }

		public TestSystemViewModel(IThreadNotifier uiNotifier, string testName, ITestSysAsync asyncTest) {
			_uiNotifier = uiNotifier;
			TestName = testName;
			AssociatedWithModelTest = asyncTest;


			_runTestAsync = new DependedCommand(RunTest, () => !IsRunningTest);
			_cancelTestAsync = new DependedCommand(CancelTest, () => IsRunningTest);
		}

		private void CancelTest() {
			throw new NotImplementedException();
		}

		private void RunTest() {
			AssociatedWithModelTest.BeginTest(() => {
					// TODO: test has been really started
				},
				testResult => {
					switch (testResult) {
						case TestSysResult.Success:
							// TODO:
							break;
						case TestSysResult.Fail:
							// TODO:
							break;
						case TestSysResult.Canceled:
							// TODO:
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(testResult), testResult, null);
					}
				});
		}

		public bool IsRunningTest {
			get => _isRunningTest;
			set => SetProp(() => _isRunningTest != value, () => _isRunningTest = value, () => IsRunningTest);
		}


		public ICommand RunTestAsync => _runTestAsync;
		public ICommand CancelTestAsync => _cancelTestAsync;
	}
}