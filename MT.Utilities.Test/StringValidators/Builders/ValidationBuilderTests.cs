using Xunit;
using MT.Utilities.StringValidators.Builders;
using MT.Utilities.StringValidators.Strategies;
using MT.Utilities.Results;

namespace MT.Utilities.Test.StringValidators.Builders
{
    public class ValidationBuilderTests
    {
        [Fact]
        public void ValidateAll_多策略_回傳所有錯誤訊息()
        {
            var builder = new ValidationBuilder()
                .AddStrategy(new MinLengthValidationStrategy(5))
                .AddStrategy(new EmailValidationStrategy());
            var result = builder.ValidateAll("a@b");
            Assert.False(result.IsSuccess);
            Assert.Contains("長度需大於等於5字元", result.ErrorMessage);
            Assert.Contains("Email 格式錯誤", result.ErrorMessage);
        }

        [Fact]
        public void ValidateAll_全部通過_回傳Success()
        {
            var builder = new ValidationBuilder()
                .AddStrategy(new MinLengthValidationStrategy(5))
                .AddStrategy(new EmailValidationStrategy());
            var result = builder.ValidateAll("abcde@x.com");
            Assert.True(result.IsSuccess);
            Assert.Equal("abcde@x.com", result.Value);
        }

        [Fact]
        public void FromJson_建立Builder_可正確驗證()
        {
            string json = "[{'Type':'MinLength','Params':{'minLength':3}},{'Type':'Email'}]".Replace("'", "\"");
            var builder = ValidationBuilder.FromJson(json);
            var result = builder.ValidateAll("a@b");
            Assert.False(result.IsSuccess);
            Assert.Contains("Email 格式錯誤", result.ErrorMessage);
        }
    }
}
