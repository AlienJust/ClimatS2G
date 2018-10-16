using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.TestSystems {
	public interface ITestSysAsync {
		void BeginTest(Action testHasBegun, Action<double, TestSysStepResult, string> progressChanged, Action<TestSysResult> testComplete);
		void CancelCurrentTest();
	}
}
