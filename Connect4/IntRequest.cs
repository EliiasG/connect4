using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    internal class IntRequest
    {
        public readonly int Value;
        public IntRequest(int min, int max) 
        {
            Value = requestNumber(min, max);
        }

        private int requestNumber(int min, int max)
        {
            while(true)
            {
                string ans = Console.ReadLine();
                int res;
                if(Int32.TryParse(ans,out res) && min <= res && max >= res) return res;
                else Console.WriteLine("Please give me a number between " + min + " and " + max);
            }
        }
    }
}
