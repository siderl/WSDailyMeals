using WSDailyMeals.Algorithm;

namespace WSDailyMeals
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceDailyMeals : IServiceDailyMeals
    {
        public Individuo GetData(string value)
        {
            double totalKCal = double.Parse(value);

            return getDiet(totalKCal);
        }

        public Individuo getDiet(double totalKCal)
        {
            double KCalProt = totalKCal * .15;
            double KCalCarbs = totalKCal * .55;
            double KCalLipid = totalKCal * .3;

            Core c = new Core(KCalLipid, KCalCarbs, KCalProt);
            return c.result;
        }

    }
}
