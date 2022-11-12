using Tabkhity.Core.Errors;

namespace Tabkhity.Core.ResponsesTypes
{
    public class PagedResponse<T> : OperationResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long pg_total { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, long pg_total = 0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Code = null;
            this.Status = OperationOutputStatus.Success;
            this.pg_total = pg_total;
        }
        public PagedResponse(HttpErrorCodes httpErrorCode, IErrorCodes errorCode, string description = "")
        {
            Code = errorCode;
            HttpErrorCode = httpErrorCode;
            ErrorMessage = description;
            Status = OperationOutputStatus.Fail;
        }
        public PagedResponse(HttpErrorCodes httpErrorCode, string description = "")
        {
            Code = CommonErrorCodes.NULL;
            HttpErrorCode = httpErrorCode;
            ErrorMessage = description;
            Status = OperationOutputStatus.Fail;
        }
    }
}
