using System;

namespace Stage0
{
    partial class Program
    {
        static void main()
        {
            Welcome2430();
            Welcome2132();
            Console.ReadKey();
        }

        private static void Welcome2430()
        {
            Console.Write("Enter your name: ");
            string name;
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first {1} colsole aplication", name);
        }

        static partial void Welcome2132();
    }
}