using System;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16 {
	internal static class KsmCommandWorkmodeExtensions {
		public static string ToText(this KsmCommandWorkmode mode) {
			switch (mode) {
				case KsmCommandWorkmode.Off:
					return "���������";
				case KsmCommandWorkmode.Auto:
					return "������ � �������������� ������";
				case KsmCommandWorkmode.Manual:
					return "������ �� ������ �������";
				case KsmCommandWorkmode.Unknown:
					return "����������� ��������";
				default:
					throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
			}
		}
	}
}