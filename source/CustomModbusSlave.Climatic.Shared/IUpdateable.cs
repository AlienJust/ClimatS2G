namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public interface IUpdateable<in T>
    {
        void Update(T updatedValue);
    }
}