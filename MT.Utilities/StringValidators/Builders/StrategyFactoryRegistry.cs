using System;
using System.Collections.Generic;
using MT.Utilities.StringValidators.Strategies;

namespace MT.Utilities.StringValidators.Builders
{
    /// <summary>
    /// 註冊所有策略的工廠方法，根據 type 字串產生對應策略。
    /// </summary>
    public static class StrategyFactoryRegistry
    {
        private static readonly Dictionary<string, Func<Dictionary<string, object>, IValidationStrategy>> _factories =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["Email"] = _ => new EmailValidationStrategy(),
                ["MinLength"] = dict =>
                {
                    var minLength = dict.ContainsKey("minLength") ? Convert.ToInt32(dict["minLength"]) : throw new ArgumentException("minLength is required");
                    return new MinLengthValidationStrategy(minLength);
                }
            };

        /// <summary>
        /// 嘗試取得對應 type 的策略工廠。
        /// </summary>
        public static bool TryGetFactory(string type, out Func<Dictionary<string, object>, IValidationStrategy>? factory)
        {
            return _factories.TryGetValue(type, out factory);
        }
    }
}
