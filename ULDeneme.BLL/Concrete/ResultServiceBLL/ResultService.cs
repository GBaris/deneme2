using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULDeneme.BLL.Concrete.ResultServiceBLL
{
    public class ResultService<T>
    {
        public bool HasError { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public T Data { get; set; }

        public ResultService()
        {
            Errors = new List<ErrorItem>();
        }

        public void AddError(string errorType, string errorMessage)
        {
            Errors.Add(new ErrorItem { ErrorType = errorType, ErrorMessage = errorMessage });
            HasError = true;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ErrorItem
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }

    }
}
