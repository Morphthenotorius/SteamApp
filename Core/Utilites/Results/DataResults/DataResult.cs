using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results.DataResults
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(T data,bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }

        public DataResult(T data,bool isSuccess, string message) : base(isSuccess, message)
        {
            Data= data;
        }


    }
}
