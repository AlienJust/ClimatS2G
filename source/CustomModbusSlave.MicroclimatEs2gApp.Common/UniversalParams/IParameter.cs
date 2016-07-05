namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams {
	public interface IParameter : IGroupItem {
		IGroup OriginGroup { get; }
	}
}