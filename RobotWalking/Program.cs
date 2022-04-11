using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RobotWalking
{
    class ChessLocation
    {
        public int _myselfIndex = -1;
    }

    class Program
    {
        static int rows = 6;
        static int columns = 8;

        static string getLastLoop(string loop)
        {
            StringBuilder ret = new StringBuilder();

            //The matrix will be initalized with -1 because it is a invalid index for an array.
            //This value will be used for control
            int[,] table = new int[rows, columns];
            for (int i = 0; i < rows; i++)
                for(int j = 0; j < columns; j++)
                    table[i, j] = -1;

            char[] cmds = loop.ToCharArray();

            int x = 4;
            int y = 0;
            table[x, y] = 0;

            for (int i = 0; i < cmds.Length; i++)
            {
                switch (cmds[i])
                {
                    case 'R': y++; break;
                    case 'L': y--; break;
                    case 'U': x++; break;
                    case 'D': x--; break;
                }

                //If the coordinate [x,y] was never visited, its value will be -1. So, save in this place the current index of the array
                if(table[x, y] == -1)
                    table[x, y] = i;
                else
                {
                    //When finds the first coordinate already visited, it uses the indexes to run a for until de end
                    for(int j = table[x, y] + 1; j <= i; j++)
                        ret.Append(cmds[j]);

                    break;
                }
            }

            return ret.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(getLastLoop("RRRRDDDLLUUUUUUURRDDDDR"));
        }
    }
}
