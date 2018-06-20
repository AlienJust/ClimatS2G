using System;
using AlienJust.Support.Concurrent.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.TestSystems;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.TestSys {
	public class Freeze100Test : ITestSysAsync {
		private readonly IWorker<Action> _sharedWorker;
		private bool _isTestCanceled;

		public Freeze100Test(IWorker<Action> sharedWorker, ICmdListenerStd evaporrator03Listener) {
			_sharedWorker = sharedWorker;
			_isTestCanceled = false;
		}

		public void BeginTest(Action testHasBegun, Action<TestSysResult> testComplete) {
			_sharedWorker.AddWork(() => {
				testHasBegun.Invoke();
				var result = TestSysResult.Canceled;
				

				testComplete.Invoke(result);
			});
		}

		public void CancelCurrentTest()
		{
			_isTestCanceled = true;
		}
	}
}
