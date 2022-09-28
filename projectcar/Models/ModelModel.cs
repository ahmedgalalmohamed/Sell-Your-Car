using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectcar.Models
{
    public class ModelModel
    {
        public int Id { get; set; }
        [MaxLength(25, ErrorMessage = "Must First Name Is max 20 character")]
        [MinLength(2, ErrorMessage = "Must First Name Is min 2 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [Range(2000,2022,ErrorMessage ="Year Model 2000-2022")]
        public int Yearmodel { get; set; }
 
        public int MakerId { get; set; }


        public int BodyStyleId { get; set; }

        public MakerModel Maker { get; set; }
        public BodyStyleModel BodyStyle { get; set; }

        public ICollection<CarModel> Cars { get; set; }

    }
}
