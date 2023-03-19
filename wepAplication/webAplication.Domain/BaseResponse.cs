using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain;

public class BaseResponse<T>: IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}

// class test
// {
//     void test1()
//     {
//         var adminE = new Admin.Entity();
//         adminE.Id = "asdasd";
//         var admin = adminE.ToInstance();
//         admin.
//     }
// }