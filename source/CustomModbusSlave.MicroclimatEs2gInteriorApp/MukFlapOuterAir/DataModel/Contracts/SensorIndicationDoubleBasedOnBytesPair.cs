using System;
using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts {
	class SensorIndicationDoubleBasedOnBytesPair : ISensorIndication<double> {
		private readonly BytesPair _valueFirstHi;
		private readonly double _modifier;
		private readonly double _afterModificationAddition;
		private readonly BytesPair _noLinkCode;

		public SensorIndicationDoubleBasedOnBytesPair(BytesPair valueFirstHi, double modifier, double afterModificationAddition, BytesPair noLinkCode) {
			_valueFirstHi = valueFirstHi;
			_modifier = modifier;
			_afterModificationAddition = afterModificationAddition;
			_noLinkCode = noLinkCode;
		}

		public bool NoLinkWithSensor => _valueFirstHi == _noLinkCode;

		public double Indication 
		{
			get
			{
				if (NoLinkWithSensor) throw new Exception("Нет связи с датчиком");
				return _valueFirstHi.HighFirstSignedValue*_modifier + _afterModificationAddition;
			}
		}
	}
}