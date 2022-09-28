using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectcar.Models
{
    public class CarModel
    {
        [MinLength(5, ErrorMessage = "Must  Location Is VIN 5 character")]
        [Required(ErrorMessage = "This Field Required")]
        [RegularExpression(@"^[A-HJ-NPR-Za-hj-npr-z\d]{8}[\dX][A-HJ-NPR-Za-hj-npr-z\d]{2}\d{6}$",ErrorMessage = "Not Match VIN For Car ex(4Y1SL65848Z411439)")]
        public string VIN { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [Range(50000,1000000,ErrorMessage ="Please Enter Price 50000 to 1M")]
        public int price { get; set; }
        [MaxLength(50, ErrorMessage = "Must  Location Is max 50 character")]
        [MinLength(5, ErrorMessage = "Must  Location Is min 5 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Location { get; set; }
        public string Fuel { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [Range(100, 500, ErrorMessage = "Please Enter Price 100 to 500 K/h")]
        public int Speed { get; set; }
        public DateTime AddWebsit { get; set; } = DateTime.Now;
        public string ShiftLever { get; set; }
        public string Wheels { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        public string Color { get; set; }
        public string Usage { get; set; }
        public string Img { get; set; }

        public int UserId { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        public int ModelId { get; set; }

        public UserModel User { get; set; }

        public ModelModel Model { get; set; }

    }
}
