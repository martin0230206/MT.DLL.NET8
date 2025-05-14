using System;

namespace MT.Utilities.StringValidators.Strategies
{
    /// <summary>
    /// 最小長度驗證策略，可自訂最小長度與錯誤訊息委派。
    /// </summary>
    public class MinLengthValidationStrategy : IStringValidationStrategy
    {
        public Delegate? ErrorMessageDelegate { get; set; }
        /// <summary>
        /// 最小長度。
        /// </summary>
        private readonly int _minLength;

        /// <summary>
        /// 建立最小長度驗證策略。
        /// </summary>
        /// <param name="minLength">最小長度。</param>
        /// <param name="errorMessageDelegate">錯誤訊息委派，參數為最小長度與驗證值。</param>
        public MinLengthValidationStrategy(int minLength, Func<int, string, string>? errorMessageDelegate = null)
        {
            _minLength = minLength;
            ErrorMessageDelegate = errorMessageDelegate;
        }

        /// <summary>
        /// 驗證指定字串長度是否大於等於最小長度。
        /// </summary>
        /// <param name="value">要驗證的字串值。</param>
        /// <returns>通過回傳 null，否則回傳錯誤訊息。</returns>
        public string? Validate(string? value)
        {
            if (value == null || value.Length < _minLength)
            {
                if (ErrorMessageDelegate is Func<int, string, string> del)
                    return del(_minLength, value ?? string.Empty);
                return $"長度需大於等於{_minLength}字元";
            }
            return null;
        }
    }
}
