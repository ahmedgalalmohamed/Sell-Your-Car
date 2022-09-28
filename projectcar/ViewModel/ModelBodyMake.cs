using projectcar.Models;
namespace projectcar.ViewModel
{
    public class ModelBodyMake
    {
        public List<BodyStyleModel> BodyStyles { get; set; }
        public List<MakerModel> Makers { get; set; }
        public List<ModelModel> Models { get; set; }
        public ModelModel Model { get; set; }
    }
}
