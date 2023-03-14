using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces
{
    public interface INstance<T>
    {
        IEntity ToEntity();

        public static T FromEntity()
        {
            throw new Exception();
        }
    }   
}