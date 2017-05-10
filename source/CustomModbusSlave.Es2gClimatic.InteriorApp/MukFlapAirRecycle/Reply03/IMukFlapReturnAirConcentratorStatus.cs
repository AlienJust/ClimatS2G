namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal interface IMukFlapReturnAirConcentratorStatus {
		/// <summary>
		/// команда на ПЛМ (1 - «включение»)
		/// </summary>
		bool CommandToPal { get; }

		/// <summary>
		/// команда с ПЛМ (1- включение ключа)
		/// </summary>
		bool CommandFromPal { get; }

		/// <summary>
		/// работа / неисправность
		/// </summary>
		bool WorkOrError { get; }

		/// <summary>
		/// ошибка по неответу драйвера
		/// </summary>
		bool ErrorNoAnswerFromDriver { get; }

		/// <summary>
		/// ошибка по току CC
		/// </summary>
		bool ErrorByCurrentCc { get; }

		/// <summary>
		/// значение компаратора
		/// </summary>
		bool ComparatorValue { get; }

		/// <summary>
		/// резерв
		/// </summary>
		bool Reserve { get; }

		/// <summary>
		/// адрес
		/// </summary>
		bool Address { get; }
	}
}