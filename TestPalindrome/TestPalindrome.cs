using Palindrome;
using Xunit;

namespace TestPalindrome
{
    public class TestPalindrome
    {
        [Theory]
        [InlineData("carroaco", Result.YES)]
        [InlineData("anna", Result.YES)]
        [InlineData("civic", Result.YES)]
        [InlineData("kayak", Result.YES)]
        [InlineData("rotator", Result.YES)]
        [InlineData("abcabcabc", Result.NO)]
        [InlineData("joao", Result.NO)]
        [InlineData("maca", Result.NO)]
        [InlineData("parangaricutirimirruaro", Result.NO)]
        public void DefaultTest(string palindrome, Result expectedResult)
        {
            var result = PalindromeValidator.isPalindromePermutation(palindrome);

            Assert.Equal(expectedResult.ToString(), result);
        }
    }
}