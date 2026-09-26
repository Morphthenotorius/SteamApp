using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public class Result : IResult
    {
        public Result (bool isSuccess,string message)
        {
            IsSuccess=isSuccess;
            Message=message;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = string.Empty;
        }
        public bool IsSuccess { get ; }
        public string Message { get ; }
    }
}
