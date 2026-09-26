using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results.DataResults
{
    public interface IDataResult<T> :IResult
    {
        T Data{ get; }
    }
}
