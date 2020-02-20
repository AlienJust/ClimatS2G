namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts.Enums
{
    enum MukFlapWorkmodeStage
    {
        ControllerInitialization, // 0
        FlapTesting, // 1
        WorkMode, // 2
        WorkModeWithFailedFlap, // 3
        WorkModePwmCorrectionBack, // 4
        NoKsm, // 5

        /// <summary>
        /// 6 - режим работы по уставке с пульта
        /// </summary>
        RemoteControlSettig,

        /// <summary>
        /// 7 - закрытие заслонки в режиме мойки
        /// </summary>
        FlapCloseWhileWashing,
        Unknown // any other value
    }
}