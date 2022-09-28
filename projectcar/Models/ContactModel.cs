using System.ComponentModel.DataAnnotations;

namespace projectcar.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Must Email Is max 50 character")]
        [Required(ErrorMessage = "This Field Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Must First Name Is max 50 character")]
        [MinLength(5, ErrorMessage = "Must First Name Is min 5 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Error Phone To Egypt")]
        public string Phone { get; set; }
        [MaxLength(160, ErrorMessage = "Must First Name Is max 160 Character")]
        [MinLength(10, ErrorMessage = "Must First Name Is min 10 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Comments { get; set; }

    }
}
