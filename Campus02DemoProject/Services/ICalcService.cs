using Campus02DemoProject.Models;

namespace Campus02DemoProject.Services
{
    public interface ICalcService
    {
        int Sum(CalcModel model);
    }

    public class CalcService : ICalcService
    {
        public int Sum(CalcModel model)
        {
            return model.Zahl1 + model.Zahl2;
        }
    }
}
