using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infra
{
    public class ServerResult
    {
        public ServerResult()
        {

        }
        public ServerResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; set; }

        public bool HasValidationError { get; set; }

        public string Message { get; set; }

        public Object Data { get; set; }

        public ErrorCode? ErrorCode { get; set; }
        //public int TotalRecord { get; set; }

        //public int Id { get; set; }		
    }
    public enum ErrorCode
    {
        NotFound = 1,
        NotValid = 2,
        AlternativeProductIsNotDetermined = 3,
        DuplicateKey = 4,
        AlreadyExists = 5
    }
}
