namespace webAplication.Domain.Interfaces
{
    public interface IInstance<out T> where T : IInstance<T>.IEntity<IInstance<T>>
    {
        T ToEntity();
        public interface IEntity<out TI> where TI : IInstance<T>
        {
            TI ToInstance();
        }
    }   
}