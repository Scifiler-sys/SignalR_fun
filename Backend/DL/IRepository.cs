namespace DL
{
    public interface IRepository<T>
    {
        public List<T> read();
    }
}