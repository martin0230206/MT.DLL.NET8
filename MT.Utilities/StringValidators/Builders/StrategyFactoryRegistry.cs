using System;
using System.Collections.Generic;
using System.Text.Json;
using MT.Utilities.StringValidators.Strategies;

namespace MT.Utilities.StringValidators.Builders
{
    /// <summary>
    /// 註冊所有策略的工廠方法，根據 type 字串產生對應策略。
    /// </summary>
    public static class StrategyFactoryRegistry
    {
        internal static int GetMinLengthFromParams(Dictionary<string, object> dict)
        {
            object minLengthObj = dict["minLength"];
            if (minLengthObj is JsonElement je && je.ValueKind == JsonValueKind.Number)
            {
                return je.GetInt32();
            }
            return Convert.ToInt32(minLengthObj);
        }

        private static readonly Dictionary<string, Func<Dictionary<string, object>, IValidationStrategy>> _factories =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["Email"] = _ => new EmailValidationStrategy(),
                ["MinLength"] = dict =>
                {
                    int minLength = GetMinLengthFromParams(dict);
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
