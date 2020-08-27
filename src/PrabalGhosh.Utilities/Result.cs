namespace PrabalGhosh.Utilities
{
    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public Error? Error { get; set; }

        public Result()
        {

        }

        public Result(T data)
        {
            Data = data;
        }

        public Result(string errorCode, string errorDescription)
        {
            Error = new Error
            {
                ErrorCode = errorCode,
                ErrorDescription = errorDescription
            };
        }

        public Result(T data, Error error)
        {
            Data = data;
            Error = error;
        }
    }
}
