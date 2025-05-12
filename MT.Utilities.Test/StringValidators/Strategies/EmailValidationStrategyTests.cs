using Xunit;
using MT.Utilities.StringValidators.Strategies;

namespace MT.Utilities.Test.StringValidators.Strategies
{
    public class EmailValidationStrategyTests
    {
        [Fact]
        public void Validate_空字串_回傳錯誤訊息()
        {
            var strategy = new EmailValidationStrategy();
            var result = strategy.Validate("");
            Assert.Equal("Email 不可為空", result);
        }

        [Fact]
        public void Validate_格式錯誤_回傳錯誤訊息()
        {
            var strategy = new EmailValidationStrategy();
            var result = strategy.Validate("abc@abc");
            Assert.Equal("Email 格式錯誤", result);
        }

        [Fact]
        public void Validate_格式正確_回傳null()
        {
            var strategy = new EmailValidationStrategy();
            var result = strategy.Validate("test@example.com");
            Assert.Null(result);
        }
    }
}
