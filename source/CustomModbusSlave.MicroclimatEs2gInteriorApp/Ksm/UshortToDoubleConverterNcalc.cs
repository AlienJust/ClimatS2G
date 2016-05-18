using NCalc;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class UshortToDoubleConverterNcalc : IUshortToDoubleConverter {
		private readonly string _ncalcExpressionText;
		public UshortToDoubleConverterNcalc(string ncalcExpressionText) {
			_ncalcExpressionText = ncalcExpressionText;
		}

		public double? Convert(ushort? value) {
			if (!value.HasValue) return null;
			var expr = new Expression(_ncalcExpressionText);
			expr.Parameters.Add("UshRv", value.Value);
			return (double) expr.Evaluate();
		}
	}
}