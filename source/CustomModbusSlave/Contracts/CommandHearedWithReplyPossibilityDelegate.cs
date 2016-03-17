namespace CustomModbusSlave.Contracts {
	public delegate void CommandHearedWithReplyPossibilityDelegate(ICommandPart commandPart, ISendAbility sendAbility);
}