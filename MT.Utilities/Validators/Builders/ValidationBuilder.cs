using System.Collections.Generic;
using MT.Utilities.Results;
using MT.Utilities.Validators.Strategies;

namespace MT.Utilities.Validators.Builders
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
        /// 執行所有驗證策略，回傳所有錯誤訊息（List 形式）。
        /// </summary>
        /// <param name="value">要驗證的字串值。</param>
        /// <returns>Tuple: 是否成功, 錯誤訊息清單。</returns>
        public (bool IsSuccess, List<string> Errors) ValidateAllWithList(string? value)
        {
            var errors = new List<string>();
            foreach (var strategy in _strategies)
            {
                var error = strategy.Validate(value);
                if (!string.IsNullOrWhiteSpace(error))
                    errors.Add(error);
            }
            return (errors.Count == 0, errors);
        }
    }
}
