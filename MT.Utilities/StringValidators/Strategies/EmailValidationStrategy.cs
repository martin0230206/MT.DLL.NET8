using System.Text.RegularExpressions;

namespace MT.Utilities.StringValidators.Strategies
{
    /// <summary>
    /// Email 驗證策略，判斷字串是否為合法 Email 格式。
    /// </summary>
    public partial class EmailValidationStrategy : IValidationStrategy
    {
        public Delegate? ErrorMessageDelegate { get; set; }

        /// <summary>
        /// Email 驗證策略
        /// </summary>
        /// <param name="errorMessageDelegate"></param>
        public EmailValidationStrategy(Func<string, string>? errorMessageDelegate = null)
        {
            ErrorMessageDelegate = errorMessageDelegate;
        }

        /// <summary>
        /// 驗證指定的字串是否為合法 Email。
        /// </summary>
        /// <param name="value">要驗證的字串值。</param>
        /// <returns>通過回傳 null，否則回傳錯誤訊息。</returns>
        public string? Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (ErrorMessageDelegate is Func<string, string> del)
                    return del(value ?? string.Empty);
                return "Email 不可為空";
            }
            if (!EmailRegex().IsMatch(value))
            {
                if (ErrorMessageDelegate is Func<string, string> del)
                    return del(value);
                return "Email 格式錯誤";
            }
            return null;
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();
    }
}
