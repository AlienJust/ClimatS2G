﻿using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire {
	public class SensorIndicationDoubleBasedOnBytesPair : SensorIndicationBytesPairCheck<double> {
		private readonly BytesPair _valueFirstHi;
		private readonly double _modifier;
		private readonly double _afterModificationAddition;

		public SensorIndicationDoubleBasedOnBytesPair(
			BytesPair valueFirstHi,
			double modifier,
			double afterModificationAddition,
			BytesPair noLinkCode) : base(valueFirstHi, noLinkCode) {
			_valueFirstHi = valueFirstHi;
			_modifier = modifier;
			_afterModificationAddition = afterModificationAddition;
		}


		protected override double GetIndication() {
			return _valueFirstHi.HighFirstSignedValue * _modifier + _afterModificationAddition;
		}

		public override string ToString() {
			if (NoLinkWithSensor) return "Обрыв датчика";
			return GetIndication().ToString("f2");
		}
	}
}
