using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCons.Sample_logic
{
    public class Generate
    {
        public void JajarGenjang()
        {
            int layer = 6;
            for (int row = 0; row < layer; row++)
            {
                for (int space = 0; space < (layer - row); space++)
                {
                    Console.Write(" ");
                }
                for (int ct = 0; ct < 5; ct++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        public void BlanckCenter()
        {
            int layer = 6;
            for (int i = 0; i < layer; i++)
            {
                for (int space = 0; space < (layer - i); space++)
                {
                    Console.Write(" ");
                }
                if (i == 5)
                {
                    for (int st = 0; st <= i; st++)
                    {
                        if (st % 2 == 0)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }
                    for (int dc = 1; dc <= i; dc++)
                    {
                        if (dc % 2 == 1)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }
                }
                else
                {
                    for (int st = 0; st <= i; st++)
                    {
                        if (st == 0)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }
                    for (int dc = 1; dc <= i; dc++)
                    {
                        if (dc == i)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Pyramid()
        {

            int layer = 6;
            for (int i = 0; i < layer; i++)
            {
                for (int space = 0; space < (layer - i); space++)
                {
                    Console.Write(" ");
                }
                for (int st = 0; st <= i; st++)
                {
                    Console.Write("*");
                }
                for (int dc = 1; dc <= i; dc++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
