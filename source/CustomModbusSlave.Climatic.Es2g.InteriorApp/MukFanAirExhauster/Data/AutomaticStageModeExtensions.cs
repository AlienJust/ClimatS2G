using System;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data {
	static class AutomaticStageModeExtensions {
		public static string ToText(this AutomaticWorkmodeStage value) {
			switch (value) {
				case AutomaticWorkmodeStage.ControllerInitialization:
					return "РёРЅРёС†РёР°Р»РёР·Р°С†РёСЏ РєРѕРЅС‚СЂРѕР»Р»РµСЂР°";
				case AutomaticWorkmodeStage.WaitingForPowerOnCommand:
					return "РѕР¶РёРґР°РЅРёРµ РєРѕРјР°РЅРґС‹ РЅР° РІРєР»СЋС‡РµРЅРёРµ";
				case AutomaticWorkmodeStage.WorkingWithFanOnByTable:
					return "СЂР°Р±РѕС‚Р° СЃ РІРєР»СЋС‡РµРЅРёРµРј РІРµРЅС‚РёР»СЏС‚РѕСЂР° РїРѕ С‚Р°Р±Р»РёС†Рµ";
				case AutomaticWorkmodeStage.Unknown:
					return "РЅРµРёР·РІРµСЃС‚РЅРѕ";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), "РЅРµРёР·РІРµСЃС‚РЅРѕРµ Р·РЅР°С‡РµРЅРёРµ");
			}
		}
	}
}
