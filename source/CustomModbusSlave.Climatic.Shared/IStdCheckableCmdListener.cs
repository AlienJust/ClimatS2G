namespace CustomModbusSlave.Es2gClimatic.Shared
{
    /// <summary>
    /// Defines common identifiers for command listeners to check them
    /// </summary>
    public interface IStdCheckableCmdListener
    {
        /// <summary>
        /// Address for checking
        /// </summary>
        byte AddrToCheck { get; }

        /// <summary>
        /// Command code for checking
        /// </summary>
        byte CodeToCheck { get; }

        /// <summary>
        /// Command length in bytes for checking
        /// </summary>
        int Length { get; }
    }
}