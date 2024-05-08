namespace InterRedBE.UTILS
{
    public class OperationResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public OperationResponse(int code, string message = "", T data = default)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }

    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Result = result
            };
        }

        public static OperationResult<T> Failure(string errorMessage)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }

    public static class OperationResponseExtensions
    {
        public static OperationResult<T> ToOperationResult<T>(this OperationResponse<T> response)
        {
            if (response.Code == 200) // Éxito
            {
                return OperationResult<T>.Success(response.Data);
            }
            else
            {
                return OperationResult<T>.Failure(response.Message);
            }
        }
    }
}
