using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.Numeric.Bits;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	internal sealed class MukWarmFloorDiagnostic1Builder : IBuilderOneToOne<int, IMukWarmFloorDiagnostic1> {
		public IMukWarmFloorDiagnostic1 Build(int source) {
			return new MukWarmFloorDiagnostic1Simple(source.GetBit(3));
		}
	}
}