using System.ComponentModel.DataAnnotations;

namespace projectcar.Models
{
    public class MakerModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Must  Name Is max 50 character")]
        [MinLength(2, ErrorMessage = "Must  Name Is min 2 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage = "Must  Location Is max 50 character")]
        [MinLength(5, ErrorMessage = "Must  Location Is min 5 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Location { get; set; }
        public ICollection<ModelModel> Models { get; set; }

    }
}
