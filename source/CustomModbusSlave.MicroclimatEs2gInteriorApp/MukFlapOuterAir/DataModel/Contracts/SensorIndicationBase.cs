using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts {
	class SensorIndicationBase<T> : ISensorIndication<T> {
		private T _indication;
		public bool NoLinkWithSensor { get; set; }

		public T Indication
		{
			get
			{
				if (!NoLinkWithSensor) 
					throw new Exception("��� ����� � ��������");
				return _indication;
			}
			set { _indication = value; }
		}
	}
}