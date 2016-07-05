namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams {
	public class ParameterSimple : IParameter {
		public ParameterSimple(string name, IGroup originGroup) {
			Name = name;
			OriginGroup = originGroup;
		}

		public string Name { get; }
		public IGroup OriginGroup { get; }
	}
}