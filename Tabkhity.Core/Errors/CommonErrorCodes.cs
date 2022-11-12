namespace Tabkhity.Core.Errors
{
    public class CommonErrorCodes : IErrorCodes
    {
        public static readonly CommonErrorCodes NULL = new CommonErrorCodes("NULL", CommonErrorCode.NULL);
        public static readonly CommonErrorCodes OPERATION_FAILED = new CommonErrorCodes("OPERATION_FAILED", CommonErrorCode.OPERATION_FAILED);
        public static readonly CommonErrorCodes INVALID_INPUT = new CommonErrorCodes("INVALID_INPUT", CommonErrorCode.INVALID_INPUT);
        public static readonly CommonErrorCodes SERVER_ERROR = new CommonErrorCodes("SERVER_ERROR", CommonErrorCode.SERVER_ERROR);
        public static readonly CommonErrorCodes INVALID_EMAIL_OR_PASSWORD = new CommonErrorCodes("INVALID_EMAIL_OR_PASSWORD", CommonErrorCode.INVALID_EMAIL_OR_PASSWORD);
        public static readonly CommonErrorCodes NOT_AUTHORIZED = new CommonErrorCodes("NotAuthorized", CommonErrorCode.NOT_AUTHORIZED);
        public static readonly CommonErrorCodes NOT_FOUND = new CommonErrorCodes("NOT_FOUND", CommonErrorCode.NOT_FOUND);

        private CommonErrorCodes(string value, CommonErrorCode code)
        {
            Value = value;
            Code = (int)code;
        }

        public CommonErrorCodes()
        {
        }

        public string Value { get; set; }
        public int Code { get; set; }
    }

    public enum CommonErrorCode
    {
        NULL = 0001,
        OPERATION_FAILED = 0003,
        INVALID_INPUT = 0004,
        SERVER_ERROR = 0006,
        INVALID_EMAIL_OR_PASSWORD = 0008,
        NOT_AUTHORIZED = 0012,
        NOT_FOUND = 0013,
    }
}
