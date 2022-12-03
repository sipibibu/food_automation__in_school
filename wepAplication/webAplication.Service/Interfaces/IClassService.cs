using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;

namespace webAplication.Service.Interfaces
{
    public interface IClassService
    {
        BaseResponse<Class> CreateClass(Class _class);
    }
}
