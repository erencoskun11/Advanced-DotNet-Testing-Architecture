namespace Unit_Test_API
{
    public class Calculator
    {
        private readonly ICalculatorService _calculatorService;
        public Calculator(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }



        public int add(int x, int y)
        {

         return _calculatorService.add(x, y);
        }
    }
}
