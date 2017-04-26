using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan {
	public class BytesPairNullableToStringThroughVaporizerFanWorkstageConverter : IBytesPairNullableSomethingConverter<string> {


		public string ConvertToSomething(BytesPair? value) {
			if (!value.HasValue) return "?";

			var val = value.Value.HighFirstUnsignedValue;
			var result = $"{val} - ";
			switch (val) {
				case 0:
					result += "������������� �����������";
					break;
				case 1:
					result += "���� �����������";
					break;
				case 2:
					result += "������� ����� � ��������� ������������";
					break;
				case 3:
					result += "����� ������ � ����������� ������������";
					break;
				case 4:
					result += "���������� ���� ���� � ��� �� ���������� ������ �� �����������";
					break;
				default:
					result += "����������";
					break;
			}

			return result;
		}
	}
}