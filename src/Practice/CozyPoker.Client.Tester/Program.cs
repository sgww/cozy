﻿using CozyPoker.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Client.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGameBullfight();
        }

        private static void TestGameBullfight()
        {
            GameBullfight bf = new GameBullfight();
            bf.Shuffle();
            var a = bf.Get();
            var b = bf.Get();

            Console.Write("A的牌： ");
            foreach (var i in a)
            {
                Console.Write(i.ToString());
                Console.Write("  ");
            }
            Console.WriteLine();
            Console.Write("B的牌： ");
            foreach (var i in b)
            {
                Console.Write(i.ToString());
                Console.Write("  ");
            }
            Console.WriteLine();
            if (bf.Compare(a, b))
            {
                Console.WriteLine("A胜");
            }
            else
            {
                Console.WriteLine("B胜");
            }
        }
    }
}