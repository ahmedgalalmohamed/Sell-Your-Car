using Microsoft.EntityFrameworkCore;
using projectcar.Models;

namespace projectcar.ViewModel
{
    public class ListBodyStyleVM
    {
        CarContext carContext;
        public ListBodyStyleVM(CarContext carContext)
        {
            this.carContext = carContext;
        }
        public List<BodyStyleModel> bodyStyles { get; set; }

        public List<CarModel> cars { get; set; }

        public int CountcarsForBodystyle(string Name)
        {
            BodyStyleModel bodyStyle = carContext.BodyStyles.Where(bs => bs.Name == Name).Include(bs => bs.Models).ThenInclude(m => m.Cars).SingleOrDefault();
            int count = 0;
            foreach (var item in bodyStyle.Models)
            {
                count += item.Cars.Count();
            }
            return count;
        }
    }
}
