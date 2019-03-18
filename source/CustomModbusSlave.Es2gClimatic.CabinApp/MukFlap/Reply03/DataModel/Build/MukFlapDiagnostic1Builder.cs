using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Build
{
    class MukFlapDiagnostic1Builder : IBuilder<IMukFlapDiagnostic1>
    {
        private readonly int _absoluteValue;

        public MukFlapDiagnostic1Builder(int absoluteValue)
        {
            _absoluteValue = absoluteValue;
        }

        public IMukFlapDiagnostic1 Build()
        {
            return new MukFlapDiagnostic1(
                (_absoluteValue & 0x10) == 0x10,
                (_absoluteValue & 0x40) == 0x40,
                (_absoluteValue & 0x80) == 0x80
            );
        }
    }
}