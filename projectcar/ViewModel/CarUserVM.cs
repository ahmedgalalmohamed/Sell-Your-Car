using projectcar.Models;

namespace projectcar.ViewModel
{
    public class CarUserVM
    {
        public CarModel car { get; set; }
        public List<CarModel> cars { get; set; }
        public List<ModelModel> models { get; set; }
        public List<BodyStyleModel> BodyStyles { get; set; }
        public List<MakerModel> Makers { get; set; }
    }
}
