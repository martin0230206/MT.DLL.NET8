namespace MT.Utilities.StringValidators.Strategies
{
    /// <summary>
    /// 驗證策略介面，定義驗證方法。
    /// </summary>
    public interface IStringValidationStrategy
    {
        /// <summary>
        /// 驗證指定的字串值，回傳錯誤訊息，若通過則為 null。
        /// </summary>
        /// <param name="value">要驗證的字串值。</param>
        /// <returns>通過回傳 null，否則回傳錯誤訊息。</returns>
        string? Validate(string? value);

        /// <summary>
        /// 錯誤訊息委派，可自訂錯誤訊息內容。
        /// </summary>
        Delegate? ErrorMessageDelegate { get; set; }
    }
}
