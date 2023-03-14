using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces
{
    public interface IInstance<T> where T : IEntity
    {
        T ToEntity();

        public static IInstance<T> FromEntity()
        {
            throw new Exception();
        }
    }   
}