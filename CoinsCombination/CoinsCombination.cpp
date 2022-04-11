// CoinsCombination.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

int getNumberOfCombinations(int cents)
{
    int centsArr[]{ 1, 5, 10, 20, 25, 50 };
    int count = 6;

    int matrix[6][1000]; //Uses a big array default

    for (int i = 0; i < count; i++)
    {
        for (int j = 0; j <= cents; j++)
        {
            if (j == 0)
            {
                matrix[i][j] = 1;
                continue;
            }

            if (i == 0)
            {
                matrix[i][j] = (j % centsArr[i] == 0) ? 1 : 0; //Calculates if these position is divisible by each of coins, if so, sets the value 1
                continue;
            }

            if (j >= centsArr[i]) //Calculates if the columns is greater than or equal to the current coins
            {
                matrix[i][j] = matrix[i - 1][j] + matrix[i][j - centsArr[i]]; //Sums the value stored in the row above and the value stored previously in that position
            }
            else
            {
                matrix[i][j] = matrix[i - 1][j]; //Set for the current position, the value stored in the row above
            }
        }
    }

    return matrix[count - 1][cents];
}

int main()
{
    std::cout << getNumberOfCombinations(5) << std::endl ;
    std::cout << getNumberOfCombinations(10) << std::endl ;
    std::cout << getNumberOfCombinations(50) << std::endl ;
    std::cout << getNumberOfCombinations(100) << std::endl ;
}