namespace MT.Utilities.Results
{
    /// <summary>
    /// 泛型結果類別，包含是否成功與錯誤訊息。
    /// </summary>
    /// <typeparam name="T">資料型別。</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 是否成功。
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// 失敗時的錯誤訊息。
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// 資料值。
        /// </summary>
        public T? Value { get; }

        public Result(bool isSuccess, T? value, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Fail(string errorMessage) => new(false, default, errorMessage);
    }
}
