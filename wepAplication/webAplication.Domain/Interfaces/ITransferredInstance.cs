using webAplication.DAL.Interfaces;

namespace webAplication.Domain.Interfaces;

public interface ITransferredInstance<TE, TI> :
    IInstance,
    ITransferred<TE, TI>  where TE : Entity where TI : IInstance
{
    
}