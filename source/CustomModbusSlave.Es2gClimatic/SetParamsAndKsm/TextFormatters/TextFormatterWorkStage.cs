using System;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.TextFormatters {
	public class TextFormatterWorkStage : ITextFormatter<BytesPair?> {
		public string Format(BytesPair? value) {
			if (!value.HasValue) return "--";
			return value.Value.HighFirstUnsignedValue + " - " + new KsmWorkstageBuilder(value.Value.HighFirstUnsignedValue).Build().ToText();
		}
	}

	public enum KsmWorkStage {
		TurnOff0FormingTurnOffCommandMuk, // 0
		TurnOff1Time5SecondsOut, // 1
		TurnOff2,
		TurnOff3,
		TurnOff4,

		TurnOn5FormingTurnOnCommandMuk, // 5
		TurnOn6Time100SecondsOutAndMukTestsControl, // 6
		TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 7
		TurnOn8CoolModeEmersionWorkControlAndSelfRegulator, // 8
		TurnOn9,

		Reserve10,
		Reserve11,
		Reserve12,
		Reserve13,
		Reserve14,

		Settling15,
		Settling16,
		Settling17,
		Settling18,
		Settling19,

		Service20FormingTurnOnCommandMuk, // 20
		Service21Time100SecondsAndMukTestsControl, // 21
		Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 22
		Service23CoolModeEmersionWorkControlAndSelfRegulator, // 23
		Service24,

		Washing25FormingTurnOffCommandMuk, //25
		Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl, // 26
		Washing27,
		Washing28,
		Washing29,

		ManualMode30FormingTurnOnCommandMuk, // 31
		ManualMode31Time100SecondsOutAndMukTestsControl, // 32
		ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 32
		ManualMode33CoolModeEmersionWorkControlAndSelfRegulator, // 33
		ManualMode34,

		Unknown
	}

	static class KsmWorkStageExtensions {
		public static string ToText(this KsmWorkStage workStage) {
			switch (workStage) {
				case KsmWorkStage.TurnOff0FormingTurnOffCommandMuk:
					return "����������: ������������ ������ ����������� �� ���";
				case KsmWorkStage.TurnOff1Time5SecondsOut:
					return "����������: �������� ����� 5 ������, �������� �� ���������";
				case KsmWorkStage.TurnOff2:
					return "����������: ";
				case KsmWorkStage.TurnOff3:
					return "����������: ";
				case KsmWorkStage.TurnOff4:
					return "����������: ";

					case KsmWorkStage.TurnOn5FormingTurnOnCommandMuk:
					return "���������: ������������ ������ ���������� �� ���";
				case KsmWorkStage.TurnOn6Time100SecondsOutAndMukTestsControl:
					return "���������: �������� ����� 100 ������, �������� ����������� ������ �� ���";
				case KsmWorkStage.TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "���������: ��������� ���������� �� ����������, ������� � ����� ����������� ��� ��������";
				case KsmWorkStage.TurnOn8CoolModeEmersionWorkControlAndSelfRegulator:
					return "���������: ����� ����������, �������� ����������������� Emerson, ����������� ���������";
				case KsmWorkStage.TurnOn9:
					return "���������: ";

				case KsmWorkStage.Reserve10:
					return "������: ";
				case KsmWorkStage.Reserve11:
					return "������: ";
				case KsmWorkStage.Reserve12:
					return "������: ";
				case KsmWorkStage.Reserve13:
					return "������: ";
				case KsmWorkStage.Reserve14:
					return "������: ";

				case KsmWorkStage.Settling15:
					return "������: ";
				case KsmWorkStage.Settling16:
					return "������: ";
				case KsmWorkStage.Settling17:
					return "������: ";
				case KsmWorkStage.Settling18:
					return "������: ";
				case KsmWorkStage.Settling19:
					return "������: ";

				case KsmWorkStage.Service20FormingTurnOnCommandMuk:
					return "������: ������������ ������ ���������� �� ���";
				case KsmWorkStage.Service21Time100SecondsAndMukTestsControl:
					return "������: �������� ����� 100 ������, �������� ����������� ������ �� ���";
				case KsmWorkStage.Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "������: ��������� ���������� �� ����������, ������� � ����� ����������� ��� ��������";
				case KsmWorkStage.Service23CoolModeEmersionWorkControlAndSelfRegulator:
					return "������: ����� ����������, �������� ����������������� Emerson, ����������� ���������";
				case KsmWorkStage.Service24:
					return "������: ";

				case KsmWorkStage.Washing25FormingTurnOffCommandMuk:
					return "�����: ������������ ������ ����������� �� ��� (� �.�. �������� ��������)";
				case KsmWorkStage.Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl:
					return "�����: �������� ����� 100 ������, �������� �� �������� ��������, �������� �������� ���";
				case KsmWorkStage.Washing27:
					return "�����: ";
				case KsmWorkStage.Washing28:
					return "�����: ";
				case KsmWorkStage.Washing29:
					return "�����: ";

				case KsmWorkStage.ManualMode30FormingTurnOnCommandMuk:
					return "������ �����: ������������ ������ ���������� �� ���";
				case KsmWorkStage.ManualMode31Time100SecondsOutAndMukTestsControl:
					return "������ �����: �������� ����� 100 ������, �������� ����������� ������ �� ���";
				case KsmWorkStage.ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "������ �����: ��������� ���������� �� ����������, ������� � ����� ����������� ��� ��������";
				case KsmWorkStage.ManualMode33CoolModeEmersionWorkControlAndSelfRegulator:
					return "������ �����: ����� ����������, �������� ����������������� Emerson, ����������� ���������";
				case KsmWorkStage.ManualMode34:
					return "������ �����: ";
				default:
					return "��";
			}
			throw new Exception("�����-�� �����, ���������� ��������� � ���� ����� ���� �� �������������");
		}
	}

	class KsmWorkstageBuilder : IBuilder<KsmWorkStage> {
		private readonly ushort _value;
		public KsmWorkstageBuilder(ushort value) {
			_value = value;
		}

		public KsmWorkStage Build() {
			switch (_value) {
				case 0:
					return KsmWorkStage.TurnOff0FormingTurnOffCommandMuk;
				case 1:
					return KsmWorkStage.TurnOff1Time5SecondsOut;
				case 2:
					return KsmWorkStage.TurnOff2;
				case 3:
					return KsmWorkStage.TurnOff3;
				case 4:
					return KsmWorkStage.TurnOff4;

				case 5:
					return KsmWorkStage.TurnOn5FormingTurnOnCommandMuk;
				case 6:
					return KsmWorkStage.TurnOn6Time100SecondsOutAndMukTestsControl;
				case 7:
					return KsmWorkStage.TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 8:
					return KsmWorkStage.TurnOn8CoolModeEmersionWorkControlAndSelfRegulator;
				case 9:
					return KsmWorkStage.TurnOn9;

				case 10:
					return KsmWorkStage.Reserve10;
				case 11:
					return KsmWorkStage.Reserve11;
				case 12:
					return KsmWorkStage.Reserve12;
				case 13:
					return KsmWorkStage.Reserve13;
				case 14:
					return KsmWorkStage.Reserve14;

				case 15:
					return KsmWorkStage.Settling15;
				case 16:
					return KsmWorkStage.Settling16;
				case 17:
					return KsmWorkStage.Settling17;
				case 18:
					return KsmWorkStage.Settling18;
				case 19:
					return KsmWorkStage.Settling19;


				case 20:
					return KsmWorkStage.Service20FormingTurnOnCommandMuk;
				case 21:
					return KsmWorkStage.Service21Time100SecondsAndMukTestsControl;
				case 22:
					return KsmWorkStage.Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 23:
					return KsmWorkStage.Service23CoolModeEmersionWorkControlAndSelfRegulator;
				case 24:
					return KsmWorkStage.Service24;

				case 25:
					return KsmWorkStage.Washing25FormingTurnOffCommandMuk;
				case 26:
					return KsmWorkStage.Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl;
				case 27:
					return KsmWorkStage.Washing27;
				case 28:
					return KsmWorkStage.Washing28;
				case 29:
					return KsmWorkStage.Washing29;

				case 30:
					return KsmWorkStage.ManualMode30FormingTurnOnCommandMuk;
				case 31:
					return KsmWorkStage.ManualMode31Time100SecondsOutAndMukTestsControl;
				case 32:
					return KsmWorkStage.ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 33:
					return KsmWorkStage.ManualMode33CoolModeEmersionWorkControlAndSelfRegulator;
				case 34:
					return KsmWorkStage.ManualMode34;

				default:
					return KsmWorkStage.Unknown;
			}
		}
	}
}