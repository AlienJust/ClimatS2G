using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.SensorIndications {
	public class SensorIndicationBase<T> : ISensorIndication<T> {
		private T _indication;
		public bool NoLinkWithSensor { get; set; }

		public T Indication
		{
			get
			{
				if (!NoLinkWithSensor) 
					throw new Exception("Нет связи с датчиком");
				return _indication;
			}
			set { _indication = value; }
		}
	}
}