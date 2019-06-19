namespace CustomModbusSlave.Es2gClimatic.Shared
{
    /// <summary>
    /// Строитель чего-бы то ни было
    /// </summary>
    /// <typeparam name="T">Тип объекта, который строитель строит</typeparam>
    public interface IBuilder<out T>
    {
        T Build();
    }
}