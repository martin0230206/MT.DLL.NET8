namespace MT.Utilities.StringValidators.Builders
{
    /// <summary>
    /// 用於 JSON 反序列化的驗證策略描述物件。
    /// </summary>
    public class ValidationStrategyDto
    {
        public string? Type { get; set; }
        public Dictionary<string, object>? Params { get; set; }
    }
}
