using System.Collections.Generic;
using MT.Utilities.Results;
using MT.Utilities.StringValidators.Strategies;
using MT.Utilities.StringValidators.Builders;

namespace MT.Utilities.StringValidators.Builders
{
    /// <summary>
    /// 驗證建造者，負責組合多個驗證策略並執行驗證。
    /// </summary>
    public class ValidationBuilder
    {
        /// <summary>
        /// 儲存所有加入的驗證策略。
        /// </summary>
        private readonly List<IValidationStrategy> _strategies = new();

        /// <summary>
        /// 新增一個驗證策略。
        /// </summary>
        /// <param name="strategy">要加入的驗證策略。</param>
        /// <returns>回傳自身以便鏈式呼叫。</returns>
        public ValidationBuilder AddStrategy(IValidationStrategy strategy)
        {
            _strategies.Add(strategy);
            return this;
        }

        /// <summary>
        /// 執行所有驗證策略，回傳所有錯誤訊息。
        /// </summary>
        /// <param name="value">要驗證的字串值。</param>
        /// <returns>Result 物件，包含是否成功與所有錯誤訊息。</returns>
        public Result<string> ValidateAll(string? value)
        {
            var errors = new List<string>();
            foreach (var strategy in _strategies)
            {
                var error = strategy.Validate(value);
                if (!string.IsNullOrWhiteSpace(error))
                    errors.Add(error);
            }
            if (errors.Count == 0)
                return Result<string>.Success(value ?? string.Empty);
            return Result<string>.Fail(string.Join("; ", errors));
        }

        /// <summary>
        /// 從 JSON 字串建立 ValidationBuilder 實例。
        /// </summary>
        /// <param name="json">JSON 字串，描述驗證策略清單。</param>
        /// <returns>ValidationBuilder 實例。</returns>
        public static ValidationBuilder FromJson(string json)
        {
            var builder = new ValidationBuilder();
            var items = System.Text.Json.JsonSerializer.Deserialize<List<ValidationStrategyDto>>(json);
            if (items == null) return builder;
            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Type)) continue;
                if (!StrategyFactoryRegistry.TryGetFactory(item.Type, out var factory) || factory == null)
                    continue;
                var param = item.Params ?? new Dictionary<string, object>();
                builder.AddStrategy(factory(param));
            }
            return builder;
        }
    }
}
