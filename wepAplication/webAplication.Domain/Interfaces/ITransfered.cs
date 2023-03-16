using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces;

public interface ITransferred<TE, TI> where TE : Entity where TI : IInstance
{
    public abstract TE ToEntity();
    static abstract TI ToInstance(TE entity);
}