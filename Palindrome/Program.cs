using System;
using System.Collections.Generic;

namespace Palindrome
{
    public enum Result
    {
        YES,
        NO
    }

    public class PalindromeValidator
    {
        public static string isPalindromePermutation(string word)
        {
            string miniWord = word.ToLower();
            int[] characteres = new int[30];

            foreach (int c in miniWord)
                characteres[c - 97]++; //97 is the number that represents 'a' in ascii table

            int oddCount = 0;
            foreach (int c in characteres)
            {
                if(c % 2 != 0)
                    oddCount++;

                if(oddCount > 1)
                    return Result.NO.ToString();
            }

            return Result.YES.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
