using CurrencyConvertion;
using Xunit;

namespace CurrencyConversion.Test
{
    public class CurrencyConversionValidator
    {
        [Theory]
        [InlineData(1000080, 0, "Um milhão e oitenta reais")]
        [InlineData(195195195, 10, "Cento e noventa e cinco milhões cento e noventa e cinco mil e cento e noventa e cinco reais e dez centavos")]
        [InlineData(1000000, 25, "Um milhão de reais e vinte e cinco centavos")]
        [InlineData(1001, 19, "Um mil e um reais e dezenove centavos")]
        [InlineData(191987, 50, "Cento e noventa e um mil e novecentos e oitenta e sete reais e cinquenta centavos")]
        [InlineData(1, 0, "Um real")]
        [InlineData(0, 1, "Um centavo")]
        public void DefaultConversionValidator(int reais, int centavos, string expectedResult)
        {
            var result = AmountConverter.convertAmount2Words(reais, centavos);

            Assert.Equal(result, expectedResult);
        }
    }
}