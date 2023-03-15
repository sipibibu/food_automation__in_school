using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces;

public interface ITransferred<TE, TI> where TE : IEntity where TI : IInstance
{
    public abstract TE ToEntity();
    static abstract TI FromEntity(TE entity);
}