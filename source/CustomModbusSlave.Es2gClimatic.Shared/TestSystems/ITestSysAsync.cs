using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.TestSystems {
	interface ITestSysAsync
	{
		void BeginTest(Action testHasBegun, Action<TestSysResult> testComplete);
		void CancelCurrentTest();
	}

	internal enum TestSysResult
	{
		Success,
		Fail,
		Canceled
	}

	internal sealed class TestSystemViewModel
	{
		private ITestSysAsync
	}
}
