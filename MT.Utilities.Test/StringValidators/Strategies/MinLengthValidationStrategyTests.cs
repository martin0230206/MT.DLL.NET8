using Xunit;
using MT.Utilities.StringValidators.Strategies;

namespace MT.Utilities.Test.StringValidators.Strategies
{
    public class MinLengthValidationStrategyTests
    {
        [Fact]
        public void Validate_長度不足_回傳錯誤訊息()
        {
            var strategy = new MinLengthValidationStrategy(5);
            var result = strategy.Validate("abc");
            Assert.Equal("長度需大於等於5字元", result);
        }

        [Fact]
        public void Validate_長度足夠_回傳null()
        {
            var strategy = new MinLengthValidationStrategy(3);
            var result = strategy.Validate("abcd");
            Assert.Null(result);
        }
    }
}
