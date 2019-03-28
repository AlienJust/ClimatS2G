using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	internal sealed class MukWarmFloorDiagnostic2Builder : IBuilderOneToOne<int, IMukWarmFloorDiagnostic2> {
		public IMukWarmFloorDiagnostic2 Build(int source) {
			return new MukWarmFloorDiagnostic2Simple();
		}
	}
}
