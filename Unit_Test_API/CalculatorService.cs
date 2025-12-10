using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test_API
{
    public class CalculatorService: ICalculatorService
    {
        public int add(int x, int y)
        {
            return 0;
        }

        public int multip(int a,int b)
        {
            if(a==0)
            {
                throw new Exception("a=0 olamaz");
            }
            return a * b;
        }
    }
  
}
