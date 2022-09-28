using System.ComponentModel.DataAnnotations;

namespace projectcar.Models
{
    public class BodyStyleModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Must Name Is max 50 character")]
        [MinLength(2, ErrorMessage = "Must Name Is min 2 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        public string Img { get; set; }
        public ICollection<ModelModel>  Models { get; set; }
    }
}
