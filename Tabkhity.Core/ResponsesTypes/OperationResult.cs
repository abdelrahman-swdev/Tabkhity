using Tabkhity.Core.Errors;

namespace Tabkhity.Core.ResponsesTypes
{
    public class OperationResult<T>
    {
        public OperationOutputStatus Status { get; set; }
        public T Data { get; set; }
        public IErrorCodes Code { get; set; }
        public HttpErrorCodes HttpErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSucceeded => Status == OperationOutputStatus.Success;

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>
            {
                Code = CommonErrorCodes.NULL,
                Data = result,
                Status = OperationOutputStatus.Success
            };
        }

        public static OperationResult<T> Fail(IErrorCodes errorCode, string description = "")
        {
            return new OperationResult<T>
            {
                Code = errorCode,
                HttpErrorCode = HttpErrorCodes.InvalidInput,
                ErrorMessage = description,
                Status = OperationOutputStatus.Fail
            };
        }

        public static OperationResult<T> Fail(HttpErrorCodes httpErrorCode, IErrorCodes errorCode, string description = "")
        {
            return new OperationResult<T>
            {
                Code = errorCode,
                HttpErrorCode = httpErrorCode,
                ErrorMessage = description,
                Status = OperationOutputStatus.Fail
            };
        }

        public static OperationResult<T> Fail(HttpErrorCodes httpErrorCode, string description = "")
        {
            return new OperationResult<T>
            {
                Code = CommonErrorCodes.NULL,
                HttpErrorCode = httpErrorCode,
                ErrorMessage = description,
                Status = OperationOutputStatus.Fail,
            };
        }

        public static OperationResult<T> Fail(HttpErrorCodes httpErrorCode, IErrorCodes errorCode, long? logRefNo)
        {
            return new OperationResult<T>
            {
                Code = errorCode,
                HttpErrorCode = httpErrorCode,
                ErrorMessage = $"See Log File {logRefNo}",
                Status = OperationOutputStatus.Fail
            };
        }

        public static OperationResult<T> Fail(string description)
        {
            return new OperationResult<T>
            {
                Code = CommonErrorCodes.INVALID_INPUT,
                HttpErrorCode = HttpErrorCodes.InvalidInput,
                ErrorMessage = description,
                Status = OperationOutputStatus.Fail
            };
        }

        public static OperationResult<T> ServerError(IErrorCodes errorCode, string description = "")
        {
            return new OperationResult<T>
            {
                Code = errorCode,
                ErrorMessage = description,
                Status = OperationOutputStatus.ServerError
            };
        }

        public static OperationResult<T> ServerError(Exception ex, string error = null)
        {
            return new OperationResult<T>
            {
                Code = CommonErrorCodes.SERVER_ERROR,
                HttpErrorCode = HttpErrorCodes.ServerError,
                ErrorMessage = error ?? ex.Message,
                Status = OperationOutputStatus.ServerError
            };
        }
    }

    public enum OperationOutputStatus
    {
        Success,
        Fail,
        ServerError
    }
    
}
