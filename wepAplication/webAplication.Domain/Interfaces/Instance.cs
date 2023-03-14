using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces
{
    public interface INstance<T> where T : IEntity
    {
        T ToEntity();

        public static INstance<T> FromEntity()
        {
            throw new Exception();
        }
    }   
}