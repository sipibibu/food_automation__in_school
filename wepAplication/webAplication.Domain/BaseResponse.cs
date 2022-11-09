﻿using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class BaseResponse<T>: IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}