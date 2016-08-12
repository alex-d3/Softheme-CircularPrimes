using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CircularPrimes
{
    class Program
    {
        static readonly int max = 1000000;

        static void Main(string[] args)
        {
            bool[] numbers;         // Array with indices from 0 to max. Used for looking for prime numbers.
            List<int> primes;       // Prime numbers.
            List<int> circPrimes;   // Circular prime numbers.

            numbers = new bool[max];
            primes = new List<int>();
            circPrimes = new List<int>();

            /* Looking for prime numbers */

            for (int i = 2; i < max; i++)
                numbers[i] = true;

            for (int s = 2; s < max; s++)
            {
                if (numbers[s])
                {
                    primes.Add(s);

                    for (int j = s * 2; j < max; j += s)
                        numbers[j] = false;
                }
            }

            /* Looking for circular prime numbers */

            List<int> tempCircNum = new List<int>();
            int num, digits, circ, index;
            while (primes.Count > 0)
            {
                num = primes[0];

                // Determining the number of digits
                
                digits = 1;
                while ((num /= 10) > 0)
                    digits++;

                // Determining if the number is a circular prime number

                num = primes[0];
                tempCircNum.Add(num);
                circ = 1;
                for (int j = 0; j < digits - 1; j++)
                {
                    num = (num % 10) * TenInPow(digits - 1) + num / 10; // Right circular shift

                    // Check if we have such number in array with prime numbers
                    index = primes.IndexOf(num);
                    if (index > 0)
                    {
                        circ++;
                        tempCircNum.Add(num);
                        primes.RemoveAt(index);
                    }
                    else if (index == 0)
                    {
                        circ++;
                    }
                }
                if (circ == digits)
                {
                    circPrimes.AddRange(tempCircNum);
                }

                primes.RemoveAt(0);
                tempCircNum.Clear();
            }

            primes.Clear();
            
            // Writing the result into a file

            StreamWriter sw = new StreamWriter("circular_primes.txt");
            
            sw.WriteLine("Circular primes from 0 to {0}. Total amount: {1}.", max, circPrimes.Count);
            
            for (int i = 0; i < circPrimes.Count; i++)
            {
                sw.WriteLine(circPrimes[i]);
            }
            sw.Close();
            sw.Dispose();

            Console.WriteLine("Total amount of circular prime numbers:\t{0}", circPrimes.Count);
            
            circPrimes.Clear();
        }

        static int TenInPow(int y)  // Returns y-th power of ten
        {
            int result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= 10;
            }
            return result;
        }
    }
}
